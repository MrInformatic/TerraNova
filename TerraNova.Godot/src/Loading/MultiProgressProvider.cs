using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TerraNova.Godot.Loading
{
    public class MultiProgressProvider : IProgressProvider
    {
        public double Progress
        {
            get
            {
                return ProgressProviders.Count > 0 ? ProgressProviders.Average(pProgressProvider => pProgressProvider.Progress) : 1.0f;
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