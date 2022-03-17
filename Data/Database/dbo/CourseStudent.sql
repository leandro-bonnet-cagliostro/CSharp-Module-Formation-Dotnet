CREATE TABLE [dbo].[CourseStudent]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[StudentId] INT NOT NULL,
	[CourseId] INT NOT NULL,
	[InscriptionDate] DATETIME2 NOT NULL,
	[IsPayed] BIT NULL,
	CONSTRAINT FK_CourseStudent_Course FOREIGN KEY (CourseId) REFERENCES [dbo].[Course] (Id),
	CONSTRAINT FK_CourseStudent_Student FOREIGN KEY (StudentId) REFERENCES [dbo].[Student] (Id)
)
