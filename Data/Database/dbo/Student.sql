CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[LastName] NVARCHAR(100) NOT NULL,
	[FirstName] NVARCHAR(100) NOT NULL,
	[FirstInscriptionYear] SMALLINT NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
	--CONSTRAINT UQ_EMAIL_STUDENT UNIQUE (Email, Lastname)
)

-- email: toto@gmail.com nom : toto Ok
-- email: toto@gmail.com nom : tata Ok
-- email: tata@gmail.com nom : toto Ok
-- email: toto@gmail.com nom : toto no Ok