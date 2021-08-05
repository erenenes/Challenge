using MyProject.BusinessLogic.Abstract;
using MyProject.DataAccess.Abstract;
using MyProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.BusinessLogic.Concrete
{
    public class RoundManager : IRoundService
    {
        IRoundDal _roundDal;
        public RoundManager(IRoundDal roundDal)
        {
            _roundDal = roundDal;
        }


        public Round Add(Round round)
        {
            var checkRound = GetById(round.Id);
            if (checkRound == null)
                _roundDal.Add(round);

            return round;
        }

        public Round GetById(int Id)
        {
            return _roundDal.Get(x => x.Id == Id);

        }
    }
}
