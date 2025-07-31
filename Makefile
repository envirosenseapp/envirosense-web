format:
	sqlfluff fix
	dotnet format EnviroSense.Web.csproj
ready:
	sqlfluff lint
	dotnet format --verify-no-changes --verbosity diagnostic EnviroSense.Web.csproj
