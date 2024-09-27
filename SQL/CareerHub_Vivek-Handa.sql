-- Vivek Handa 
-- Coding Challenges: CareerHub, The Job Board 

-- 1. Provide a SQL script that initializes the database for the Job Board scenario “CareerHub”. 
-- 4. Ensure the script handles potential errors, such as if the database or tables already exist.
DROP DATABASE IF EXISTS CareerHub;
CREATE DATABASE CareerHub;
USE CareerHub;

-- 2. Create tables for Companies, Jobs, Applicants and Applications. 
-- 3. Define appropriate primary keys, foreign keys, and constraints. 
CREATE TABLE IF NOT EXISTS Companies (
	CompanyID INT IDENTITY(1,1) PRIMARY KEY,
	CompanyName VARCHAR(100) NOT NULL,
	Location VARCHAR(100) NOT NULL 
);

CREATE TABLE IF NOT EXISTS Jobs (
    JobID INT IDENTITY(1,1) PRIMARY KEY ,
    CompanyID INT,
    JobTitle VARCHAR(100) NOT NULL,
    JobDescription TEXT,
    JobLocation VARCHAR(100) NOT NULL,
    Salary DECIMAL(10, 2) NOT NULL,
    JobType VARCHAR(50),
    PostedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID) 
);

CREATE TABLE IF NOT EXISTS Applicants(
	ApplicantID INT IDENTITY(1,1) PRIMARY KEY,
	FirstName VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	Email VARCHAR(150) NOT NULL,
	Phone VARCHAR(50),
	Resume TEXT
);

CREATE TABLE IF NOT EXISTS Applications (
    ApplicationID INT IDENTITY(1,1) PRIMARY KEY,  
    JobID INT,
    ApplicantID INT,
    ApplicationDate DATETIME DEFAULT GETDATE(),
    CoverLetter TEXT,
    FOREIGN KEY (JobID) REFERENCES Jobs(JobID),
    FOREIGN KEY (ApplicantID) REFERENCES Applicants(ApplicantID)
);

-- Inserting data into tables 
INSERT INTO Companies (CompanyName, Location) VALUES
('Apple', 'New York'),
('Samsung', 'San Francisco'),
('Vivo', 'Chennai'),
('Oneplus', 'Germany'),
('Moto', 'USA');

INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) VALUES
(1, 'Software Developer', 'Develop and maintain software', 'New York', 80000, 'Full-time', '2024-08-15 10:00:00'),
(2, 'Data Scientist', 'Analyze data ', 'San Francisco', 95000, 'Full-time', '2024-09-01 12:30:00'),
(3, 'Full Stack Developer', 'Build frontend and backend ', 'Chennai', 75000, 'Part-time', '2024-07-20 08:45:00'),
(4, 'Product Manager', 'Manages the product', 'Germany', 105000, 'Full-time', '2024-06-25 09:15:00'),
(5, 'Backend Engineer', 'Develop API and logic', 'India', 85000, 'Part-time', '2024-09-15 14:00:00');

INSERT INTO Applicants (FirstName, LastName, Email, Phone, Resume) VALUES
('Tanmay', 'Chocksey', 'tanmayC@example.com', '9111111111', 'Tanmay_Resume'),
('Dwij', 'Joshi', 'dwijJ@example.com', '9211111111', 'Dwij_Resume'),
('Rajesh', 'Jain', 'rajeshJ@example.com', '9311111111', 'Rajesh_Resume'),
('Divyansh', 'Porwal', 'divyanshP@example.com', '9411111111', 'Divyansh_Resume'),
('Shivam', 'Katiyar', 'shivamK@example.com', '9511111111', 'Shivam_Resume');

INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter) VALUES
(2, 1, '2024-09-18 09:00:00', 'Interested in AI'),
(3, 2, '2024-09-20 11:30:00', 'Interested in Data Scientist '),
(4, 3, '2024-08-01 10:45:00', 'Interested in Full Stack Developer.'),
(5, 4, '2024-07-10 14:15:00', 'Interested in Management'),
(6, 5, '2024-09-22 16:00:00', 'Interested in Backend Engineer');


SELECT * FROM Companies;
SELECT * FROM Jobs;
SELECT * FROM Applicants;
SELECT * FROM Applications;

-- 5. Write an SQL query to count the number of applications received for each job listing in the "Jobs" table. Display the job title and the corresponding application count. Ensure that it lists all jobs, even if they have no applications.
SELECT * FROM Jobs;
SELECT * FROM Applications;

SELECT j.JobTitle, COUNT(a.ApplicationID) AS ApplicationCount
from Jobs j
LEFT JOIN Applications a 
on j.JobID = a.JobID
GROUP BY j.JobID, j.JobTitle
ORDER BY ApplicationCount DESC;

-- 6. Develop an SQL query that retrieves job listings from the "Jobs" table within a specified salary range. Allow parameters for the minimum and maximum salary values. Display the job title, company name, location, and salary for each matching job.
SELECT * FROM Jobs;
SELECT * FROM Companies;

SELECT j.JobTitle, c.CompanyName, j.JobLocation, j.Salary
FROM Jobs j
JOIN Companies c ON j.CompanyID = c.CompanyID
WHERE j.Salary BETWEEN 79999 AND 98000
ORDER BY j.Salary DESC;

-- 7. Write an SQL query that retrieves the job application history for a specific applicant. Allow a parameter for the ApplicantID, and return a result set with the job titles, company names, and application dates for all the jobs the applicant has applied to.
SELECT * FROM Applications;

SELECT j.JobTitle, c.CompanyName, a.ApplicationDate
FROM Applications a
JOIN Jobs j 
ON a.JobID = j.JobID
JOIN Companies c 
ON j.CompanyID = c.CompanyID
WHERE a.ApplicantID = 3
ORDER BY a.ApplicationDate DESC;

-- 8. Create an SQL query that calculates and displays the average salary offered by all companies for job listings in the "Jobs" table. Ensure that the query filters out jobs with a salary of zero
SELECT AVG(Salary) as AverageSalary
FROM Jobs
WHERE Salary > 0;

-- 9. Write an SQL query to identify the company that has posted the most job listings. Display the company name along with the count of job listings they have posted. Handle ties if multiple companies have the same maximum count.
SELECT * FROM Companies;
SELECT * FROM Jobs;

SELECT c.CompanyName, COUNT(j.JobID) AS JobCount
FROM Companies c
JOIN Jobs j ON c.CompanyID = j.CompanyID
GROUP BY c.CompanyID, c.CompanyName
HAVING COUNT(j.JobID) = (
    SELECT MAX(JobCount)
    FROM (SELECT COUNT(j.JobID) AS JobCount
          FROM Jobs j
          GROUP BY j.CompanyID)
		  AS JobCounts
);

-- 10. Find the applicants who have applied for positions in companies located in 'CityX' and have at least 3 years of experience.
SELECT * FROM jobs;
UPDATE Applicants
SET Resume = 'Interested in Data Scientist and have 3+ years of experience '
WHERE ApplicantID = 2;

SELECT a.FirstName, a.LastName, a.Email
FROM Applicants a
WHERE a.ApplicantID IN (
    SELECT app.ApplicantID
    FROM Applications app
    JOIN Jobs j 
	ON app.JobID = j.JobID
    JOIN Companies c 
	ON j.CompanyID = c.CompanyID
    WHERE c.Location = 'India'
)
AND a.Resume LIKE '%3+ years of experience%';

-- 11. Retrieve a list of distinct job titles with salaries between $60,000 and $80,000.
SELECT DISTINCT JobTitle
FROM Jobs
WHERE Salary between 60000 and 80000;

-- 12. Find the jobs that have not received any applications.
SELECT j.JobTitle
FROM Jobs j
LEFT JOIN Applications a ON j.JobID = a.JobID
WHERE a.ApplicationID IS NULL;

-- 13. Retrieve a list of job applicants along with the companies they have applied to and the positions they have applied for.
SELECT * FROM Applications;
SELECT * FROM Jobs;
SELECT * FROM Companies;

SELECT a.FirstName, a.LastName, c.CompanyName, j.JobTitle
FROM Applications app
JOIN Applicants a ON app.ApplicantID = a.ApplicantID
JOIN Jobs j ON app.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID;

-- 14. Retrieve a list of companies along with the count of jobs they have posted, even if they have not received any applications.
SELECT c.CompanyName, COUNT(j.JobID) AS JobCount
FROM Companies c
LEFT JOIN Jobs j ON c.CompanyID = j.CompanyID
GROUP BY c.CompanyID, c.CompanyName;

-- 15. List all applicants along with the companies and positions they have applied for, including those who have not applied.SELECT * FROM Applicants;
SELECT * FROM Jobs;
SELECT * FROM Applications;


SELECT a.FirstName, a.LastName, c.CompanyName, j.JobTitle
FROM Applicants a
LEFT JOIN Applications app 
ON a.ApplicantID = app.ApplicantID
LEFT JOIN Jobs j 
ON app.JobID = j.JobID
LEFT JOIN Companies c 
ON j.CompanyID = c.CompanyID;

-- 16. Find companies that have posted jobs with a salary higher than the average salary of all jobs.SELECT c.CompanyName, j.JobTitle, j.Salary
FROM Companies c
JOIN Jobs j ON c.CompanyID = j.CompanyID
WHERE j.Salary > 
(SELECT AVG(salary) FROM Jobs WHERE salary > 0);

-- 17. Display a list of applicants with their names and a concatenated string of their city and state.SELECT FirstName, LastName, CONCAT(Phone, ', ', Email) AS ContactDetails
from Applicants;

-- 18. Retrieve a list of jobs with titles containing either 'Developer' or 'Engineer'.SELECT JobTitle
FROM Jobs
WHERE JobTitle LIKE '%Developer%' OR JobTitle LIKE '%Engineer%';

-- 19. Retrieve a list of applicants and the jobs they have applied for, including those who have not applied and jobs without applicants.
SELECT a.FirstName, a.LastName, j.JobTitle
FROM Applicants a
LEFT JOIN Applications app 
ON a.ApplicantID = app.ApplicantID
LEFT JOIN Jobs j 
ON app.JobID = j.JobID;

-- 20. List all combinations of applicants and companies where the company is in a specific city and the applicant has more than 2 years of experience. For example: city=Chennai
SELECT * FROM Applicants
SELECT * FROM Jobs
SELECT * FROM Companies

SELECT a.FirstName, a.LastName, c.CompanyName
FROM Applicants a
JOIN Applications app ON a.ApplicantID = app.ApplicantID
JOIN Jobs j ON app.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID
WHERE c.Location = 'Chennai'
AND a.Resume LIKE '%2+ years of experience%'; 

-- Without condition
SELECT a.FirstName, a.LastName, c.CompanyName
FROM Applicants a
JOIN Applications app ON a.ApplicantID = app.ApplicantID
JOIN Jobs j ON app.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID

-- Question asked in Viva
-- Subquery Without any condition 
SELECT Applicants.FirstName, Applicants.LastName, 
       (SELECT Companies.CompanyName FROM Jobs 
        INNER JOIN Companies ON Jobs.CompanyID = Companies.CompanyID
        WHERE Jobs.JobID = (SELECT Applications.JobID FROM Applications 
                            WHERE Applications.ApplicantID = Applicants.ApplicantID)
       ) AS CompanyName
FROM Applicants 
WHERE Applicants.ApplicantID IN (SELECT Applications.ApplicantID FROM Applications);




-- Using Subquery with condition 
SELECT Applicants.FirstName, Applicants.LastName, Companies.CompanyName
FROM Applicants
JOIN Applications on Applicants.ApplicantID = Applications.ApplicantID
JOIN Jobs on Applications.JobID = Jobs.JobID
JOIN Companies on Jobs.CompanyID = Companies.CompanyID
WHERE Companies.Location = 'Chennai'
AND Applicants.ApplicantID IN (
    SELECT ApplicantID
    FROM Applicants
    WHERE Resume LIKE '%2+ years of experience%'
);



