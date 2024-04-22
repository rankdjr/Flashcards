CREATE TABLE [FLASHCARDS].[dbo].[tb_StudySessions] (
    SessionID INT PRIMARY KEY IDENTITY(1,1),
    StackID INT NOT NULL,
    SessionDate DATETIME NOT NULL,
    Score INT NOT NULL,                       
    FOREIGN KEY (StackID) REFERENCES tb_Stacks(StackID) ON DELETE CASCADE
);




