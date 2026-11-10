# dotnet-vendor

The **vendor's** repository of a small ASP.NET Core (.NET 10) web server: source code, tests and the pipeline that turns a
version tag into a container image. It has no database and no deployment. The customer's own repository,
[`dotnet-vendor-deploy`](../dotnet-vendor-deploy), decides which version runs where.

| Topic | How |
|-------|-----|
| Tests | xUnit (JUnit report in the pipeline) |
| Image | `dotnet publish`, then a Dockerfile on `mcr.microsoft.com/dotnet/aspnet:10.0` (non-root) |
| Versions | git tag `v1.2.3` -> image `registry.maresdemo.com/examples/dotnet-vendor:1.2.3`; pushes to `main` -> `main-<sha>` |
| Access for the customer | a read-only deploy token (`read_registry`) of this project, held by the deploy repository |

Release: `git tag v1.0.0 && git push origin v1.0.0`.
