USE SoftUni
GO

CREATE PROC usp_FindAllProjectsPerEmployee (@FirstName nvarchar(50), @LastName nvarchar(50))
AS
SELECT p.Name, p.Description, p.StartDate
FROM Projects p
JOIN EmployeesProjects ep ON p.ProjectID=ep.ProjectID
JOIN Employees e ON e.EmployeeID=ep.EmployeeID
WHERE e.FirstName=@FirstName AND e.LastName = @LastName
GO