"""Convert every Markdown file under Assets/Docs to a PDF next to it.

Usage (from the project root):
    python Tools/docs-to-pdf.py

Needs: Python `markdown` package (pip install markdown) and Google Chrome or
Microsoft Edge installed. Re-run whenever a .md file changes.
"""
import html
import os
import re
import subprocess
import sys
import tempfile
from pathlib import Path

import markdown

ROOT = Path(__file__).resolve().parent.parent
DOCS = ROOT / "Assets" / "Docs"

BROWSERS = [
    r"C:\Program Files\Google\Chrome\Application\chrome.exe",
    r"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
    r"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
    r"C:\Program Files\Microsoft\Edge\Application\msedge.exe",
    "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
    "/usr/bin/google-chrome",
    "/usr/bin/chromium",
]

CSS = """
@page { size: A4; margin: 18mm 16mm 20mm 16mm; }
html { font-size: 10.5pt; }
body { font-family: "Segoe UI", Calibri, Arial, sans-serif; color: #1f2328;
       line-height: 1.5; margin: 0; }
h1, h2, h3, h4 { line-height: 1.25; margin: 1.4em 0 0.5em; break-after: avoid; }
h1 { font-size: 1.9em; border-bottom: 2px solid #d0d7de; padding-bottom: .3em; margin-top: 0; }
h2 { font-size: 1.45em; border-bottom: 1px solid #d0d7de; padding-bottom: .25em; }
h3 { font-size: 1.2em; }
h4 { font-size: 1.05em; }
p, ul, ol, table, pre, blockquote { margin: 0 0 .8em; }
ul, ol { padding-left: 1.6em; }
li { margin: .15em 0; }
li.task { list-style: none; margin-left: -1.3em; }
li.task input { margin: 0 .45em 0 0; vertical-align: middle; }
a { color: #0969da; text-decoration: none; }
code { font-family: Consolas, "Courier New", monospace; font-size: .9em;
       background: #f2f4f6; padding: .1em .3em; border-radius: 3px; }
pre { background: #f6f8fa; border: 1px solid #d0d7de; border-radius: 5px;
      padding: .7em .9em; white-space: pre-wrap; word-break: break-word; break-inside: avoid; }
pre code { background: none; padding: 0; font-size: .85em; }
table { border-collapse: collapse; width: 100%; font-size: .92em; }
th, td { border: 1px solid #d0d7de; padding: .35em .6em; text-align: left; vertical-align: top; }
th { background: #f2f4f6; }
tr { break-inside: avoid; }
blockquote { border-left: 4px solid #d0d7de; color: #57606a; padding: 0 1em; margin-left: 0; }
hr { border: 0; border-top: 1px solid #d0d7de; margin: 1.5em 0; }
"""


def find_browser():
    for b in BROWSERS:
        if os.path.exists(b):
            return b
    sys.exit("Could not find Chrome or Edge.")


def to_html(md_text, title):
    body = markdown.markdown(
        md_text,
        extensions=["tables", "fenced_code", "sane_lists", "toc"],
        output_format="html5",
    )
    # GitHub-style task lists: "- [ ] item" / "- [x] item"
    body = re.sub(
        r"<li>\[( |x|X)\]\s*",
        lambda m: '<li class="task"><input type="checkbox"%s disabled>'
        % (" checked" if m.group(1).strip() else ""),
        body,
    )
    # Links between docs point at the PDF versions
    body = re.sub(r'href="([^":#]+)\.md(#[^"]*)?"',
                  lambda m: f'href="{m.group(1)}.pdf{m.group(2) or ""}"', body)
    return (f'<!doctype html><html><head><meta charset="utf-8">'
            f"<title>{html.escape(title)}</title><style>{CSS}</style></head>"
            f"<body>{body}</body></html>")


def main():
    browser = find_browser()
    md_files = sorted(DOCS.rglob("*.md"))
    with tempfile.TemporaryDirectory() as tmp:
        for md in md_files:
            text = md.read_text(encoding="utf-8")
            m = re.search(r"^#\s+(.+)$", text, re.M)
            title = m.group(1).strip() if m else md.stem
            tmp_html = Path(tmp) / (md.stem + ".html")
            tmp_html.write_text(to_html(text, title), encoding="utf-8")
            pdf = md.with_suffix(".pdf")
            subprocess.run(
                [browser, "--headless=new", "--disable-gpu", "--no-pdf-header-footer",
                 f"--user-data-dir={Path(tmp) / 'profile'}",
                 f"--print-to-pdf={pdf}", tmp_html.as_uri()],
                check=True, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL,
            )
            print(f"{pdf.relative_to(ROOT)}")


if __name__ == "__main__":
    main()
