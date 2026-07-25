# Main branch protection ruleset

Protect `main` with a branch ruleset that blocks deletion, force pushes, and direct updates. Require pull requests, resolved conversations, an up-to-date source branch, and passing status checks.

## Required check

The only required pull-request check emitted by `.github/workflows/pipeline.yml` is:

```text
CI
```

Do not require the tag-only `Publish NuGet` job because it does not run on pull requests.

## Merge settings

- allow squash merge;
- disable merge commits and rebase merge when consistent with repository policy;
- automatically delete merged branches.

## Verification

After changing the ruleset, confirm that a direct push to `main` is rejected, a failing `CI` check blocks merge, unresolved conversations block merge, and a branch behind `main` must be updated.
