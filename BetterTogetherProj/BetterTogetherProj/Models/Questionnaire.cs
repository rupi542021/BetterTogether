﻿using BetterTogetherProj.Models.DAL;
using project_classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_classes.Models
{
    public class Questionnaire
    {
        int questionnaireNum;
        DateTime questionnairePublish;
        DateTime endPublishDate;
        string subQr;
        Department dep;
        int numResponders;
        bool status;
        int questionnaireYear;
        List<Question> queslist;
        List<string> studentsAns;
        bool deleteMode;
        bool duplicateMode;

        public int QuestionnaireNum { get => questionnaireNum; set => questionnaireNum = value; }
        public DateTime QuestionnairePublish { get => questionnairePublish; set => questionnairePublish = value; }
        public DateTime EndPublishDate { get => endPublishDate; set => endPublishDate = value; }
        public string SubQr { get => subQr; set => subQr = value; }
        public Department Dep { get => dep; set => dep = value; }
        public int NumResponders { get => numResponders; set => numResponders = value; }
        public bool Status { get => status; set => status = value; }
        public int QuestionnaireYear { get => questionnaireYear; set => questionnaireYear = value; }
        public List<Question> Queslist { get => queslist; set => queslist = value; }
        public List<string> StudentsAns { get => studentsAns; set => studentsAns = value; }
        public bool DeleteMode { get => deleteMode; set => deleteMode = value; }
        public bool DuplicateMode { get => duplicateMode; set => duplicateMode = value; }

        public Questionnaire() { }

        public Questionnaire(int questionnaireNum, DateTime questionnairePublish, DateTime endPublishDate, string subQr, Department dep, int numResponders, bool status, int questionnaireYear, List<Question> queslist, List<string> studentsAns, bool deleteMode, bool doplicateMode)
        {
            QuestionnaireNum = questionnaireNum;
            QuestionnairePublish = questionnairePublish;
            EndPublishDate = endPublishDate;
            SubQr = subQr;
            Dep = dep;
            NumResponders = numResponders;
            Status = status;
            QuestionnaireYear = questionnaireYear;
            Queslist = queslist;
            StudentsAns = studentsAns;
            DeleteMode = deleteMode;
            DuplicateMode = duplicateMode;
        }

        public List<Questionnaire> GetQuestionnaire(int statusQR, int deleteMode)
        {
            DBServices dbs = new DBServices();
            List<Questionnaire> qrList = dbs.GetQuestionnaire(statusQR, deleteMode);
            return qrList;

        }
        public List<Questionnaire> GetAllQuestionnaire(int DepCode, int Year)
        {
            DBServicesUnit dbs = new DBServicesUnit();
            List<Questionnaire> qList = dbs.GetAllQuestionnaire(DepCode, Year);
            return qList;
        }
        public void InsertQuestionnaire()
        {
            DBServices dbs = new DBServices();
            dbs.InsertQuestionnaire(this);

        }

        public void UpdateQr()
        {
            DBServices dbs = new DBServices();
            dbs.UpdateQr(this);

        }

        public int GetQuestionnaireNum()
        {
            DBServices dbs = new DBServices();
            int qrLNum = dbs.GetQuestionnaireNum();
            return qrLNum;

        }

        public void UpdateStatusQr(int QrId)
        {
            DBServices dbs = new DBServices();
            dbs.UpdateStatusQr(QrId);
            
        }

        //public List<Questionnaire> GetRespondersbyDepAndYear()
        //{
        //    DBServices dbs = new DBServices();
        //    List<Questionnaire> qList = dbs.GetRespondersbyDepAndYear();
        //    return qList;
        //}

        public List<int> GetpercentActiveQr()
        {
            DBServices dbs = new DBServices();
            List<int> QrRatio = dbs.GetpercentActiveQr();
            return QrRatio;

        }

       
    }
}