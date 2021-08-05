using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
   public class StageManager:IStageService
    {
        IStageDal _stageDal;
        public StageManager(IStageDal stageDal)
        {
            _stageDal = stageDal;
        }

        public Stage Add(Stage stage)
        {
            var checkStage = GetById(stage.Id);
            if (checkStage == null)
                _stageDal.Add(stage);

            return stage;
        }

        public Stage GetById(int Id)
        {
            return _stageDal.Get(x => x.Id == Id);
        }
    }
}
