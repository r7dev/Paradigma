SELECT
	Dep.Nome AS Departamento,
	Pes.Nome AS Colaborador,
	Pes.Salario AS Salario

FROM Pessoa AS Pes
	INNER JOIN (
			SELECT
				MAX(Pess.Salario) AS Salario,
				Pess.DeptId
			FROM Pessoa AS Pess
			GROUP BY Pess.DeptId
		) AS Maiores
		ON Pes.DeptId = Maiores.DeptId AND Pes.Salario = Maiores.Salario

	INNER JOIN Departamento As Dep
		ON Dep.Id = Pes.DeptId
