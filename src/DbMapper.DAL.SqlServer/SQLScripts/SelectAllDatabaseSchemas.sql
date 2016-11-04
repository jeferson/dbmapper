SELECT DISTINCT
	SCH.[schema_id] AS [schema_id],
	SCH.[name] AS [schema_name]
FROM sys.tables AS TAB
	INNER JOIN sys.schemas AS SCH ON SCH.[schema_id] = TAB.[schema_id];