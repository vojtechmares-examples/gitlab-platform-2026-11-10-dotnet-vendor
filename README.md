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

## CI configuration

This pipeline needs **no CI/CD variables** to be set. It has no deployment and does not use the deploy server. It works with the variables GitLab predefines:

| Variable | Used for |
|----------|----------|
| `CI_REGISTRY`, `CI_REGISTRY_USER`, `CI_REGISTRY_PASSWORD`, `CI_REGISTRY_IMAGE` | `build-image` logs in to this project's Container Registry and pushes the image |
| `CI_DEPENDENCY_PROXY_GROUP_IMAGE_PREFIX` | Pulls the `docker` and `docker:dind` images from Docker Hub through the group's Dependency Proxy |

The customer reads the images with a read-only **deploy token** (`dotnet-vendor-deploy-pipeline`, scope `read_registry`) of this project (*Settings > Repository > Deploy tokens*). Its user name and token are the CI/CD variables `VENDOR_REGISTRY_USER` and `VENDOR_REGISTRY_TOKEN` of `dotnet-vendor-deploy`; nothing in this project reads them.
