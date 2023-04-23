﻿namespace RineaR.Shadow.Master
{
    public interface IMasterRepository
    {
        void Fetch();
        UnitSetting[] GetUnits();
        UnitSetting GetUnitByID(int id);
        CardSetting[] GetCards();
        CardSetting GetCardByID(int id);
    }
}