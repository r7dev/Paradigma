/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO Departamento
VALUES(1, 'TI'), (2, 'Vendas');

INSERT INTO Pessoa
VALUES(1, 'Joe', 70000, 1), (2, 'Henry', 80000, 2)
	, (3, 'Sam', 60000, 2), (4, 'Max', 90000, 1);
