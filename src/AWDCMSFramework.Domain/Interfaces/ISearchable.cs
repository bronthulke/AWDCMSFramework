using System;
using System.Collections.Generic;
using AWDCMSFramework.Domain;

namespace AWDCMSFramework.Domain.Interfaces
{
    public interface ISearchable
    {
        List<SearchItemInfo> GetSearchItems(string SearchTerm);
    }
}
