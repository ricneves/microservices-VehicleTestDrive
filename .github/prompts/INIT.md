# Agent init prompt template

Use this prompt when asking an agent to make changes to this repository:

- Start by exploring `AGENTS.md` and `.github/copilot-instructions.md`.
- Summarize the main entry points and the exact dotnet commands required to run or build the project.
- Propose a minimal change and list the files you'll edit before making edits.
- Run only repository-local commands (build/tests) and report outputs.

Keep responses concise and reference files by path.