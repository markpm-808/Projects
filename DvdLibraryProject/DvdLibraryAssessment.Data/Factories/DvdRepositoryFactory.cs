using DvdLibraryAssessment.Data.Interfaces;
using DvdLibraryAssessment.Data.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DvdLibraryAssessment.Data.Repositories;

namespace DvdLibraryAssessment.Data.Factories
{
    public static class DvdRepositoryFactory
    {
        public static IDvdRepository GetRepository()
        {
            switch(Settings.GetRepositoryType())
            {
                case "ADO":
                    return new DvdRepositoryADO();
                case "SampleData":
                    return new SampleDataRepository();
                default:
                    throw new Exception("Could not find valid mode configuration value");
            }
        }
    }
}
