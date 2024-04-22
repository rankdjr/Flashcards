CREATE VIEW vw_FlashCardsDisplay AS
SELECT
    CardID,
    Front,
    Back
FROM
    tb_FlashCards;