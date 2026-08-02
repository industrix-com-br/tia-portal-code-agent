<!-- code-scanning-dismissal-20260802 -->
# Code scanning dismissal report

- Main commit verified: `aac1dfac1e4672069096845751910fdb18d7ee80`
- Expected stale path: `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs`
- File exists on main: **no**

| Alert | Previous state | Path | Action | Final state | Dismissal reason |
|---:|---|---|---|---|---|
| #7 | open | `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs` | dismissed as stale | dismissed | false positive |
| #8 | open | `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs` | dismissed as stale | dismissed | false positive |
| #9 | open | `src/TiaAgent.OpenCode/Client/OpenCodeHttpClient.cs` | dismissed as stale | dismissed | false positive |

Dismissal comment: Stale CodeQL alert: the referenced file was deleted in commit ec4fed9 and is absent from main. PR #190 verified that this finding no longer applies.
