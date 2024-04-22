CREATE VIEW vw_StudySessionsWithStacks AS
SELECT
    s.SessionID,
    st.StackName,
    s.SessionDate,
    s.Score
FROM
    tb_StudySessions s
JOIN
    tb_Stacks st ON s.StackID = st.StackID;
