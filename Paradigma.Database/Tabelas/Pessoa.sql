CREATE TABLE [dbo].[Pessoa]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[Nome] VARCHAR(50) NOT NULL, 
	[Salario] DECIMAL(18, 2) NOT NULL, 
	[DeptId] INT NOT NULL, 
	CONSTRAINT [FK_Pessoa_Departamento] FOREIGN KEY ([DeptId]) REFERENCES [Pessoa]([Id]) 
)
