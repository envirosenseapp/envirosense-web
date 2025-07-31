format:
	sqlfluff fix
	dotnet format EnviroSense.Web.csproj
lint:
	sqlfluff lint
	dotnet format --verify-no-changes --verbosity diagnostic EnviroSense.Web.csproj
