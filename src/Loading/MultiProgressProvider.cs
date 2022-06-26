using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TerraNova.Loading
{
    public class MultiProgressProvider : IProgressProvider
    {
        public double Progress
        {
            get
            {
                return ProgressProviders.Average(pProgressProvider => pProgressProvider.Progress);
            }
        }

        public bool IsFinished
        {
            get
            {
                return ProgressProviders.All(pProgressProvider => pProgressProvider.IsFinished);
            }
        }

        private List<IProgressProvider> ProgressProviders { get; set; } = new List<IProgressProvider>();

        public void Register(IProgressProvider ProgressProvider)
        {
            ProgressProviders.Add(ProgressProvider);
        }
    }
}