format:
	sqlfluff fix
	dotnet format
lint:
	sqlfluff lint
	dotnet format --verify-no-changes --verbosity diagnostic
