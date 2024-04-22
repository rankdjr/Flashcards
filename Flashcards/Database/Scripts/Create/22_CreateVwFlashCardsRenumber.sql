CREATE VIEW vw_FlashCardsRenumbered AS
SELECT
    ROW_NUMBER() OVER (PARTITION BY StackID ORDER BY CardID) AS DisplayCardID,
    Front,
    Back,
    StackID
FROM
    tb_FlashCards;
