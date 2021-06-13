select * from questionnaire_P3 where doplicateMode=1
delete from questionnaire_P3 where qrCode=706 or qrCode=705
select * from question_P3
delete from question_P3 where qrCode=705 and qCode=2