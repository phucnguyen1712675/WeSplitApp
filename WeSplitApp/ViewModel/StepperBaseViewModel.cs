using MaterialDesignExtensions.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitApp.ViewModel
{
    public class StepperBaseViewModel : ViewModel
    {
        protected StepperLayout m_layout;
        protected bool m_isLinear;
        protected bool m_contentAnimationsEnabled;

        public bool ContentAnimationsEnabled
        {
            get
            {
                return m_contentAnimationsEnabled;
            }

            set
            {
                m_contentAnimationsEnabled = value;

                OnPropertyChanged(nameof(ContentAnimationsEnabled));
            }
        }

        public bool IsLinear
        {
            get
            {
                return m_isLinear;
            }

            set
            {
                m_isLinear = value;

                OnPropertyChanged(nameof(IsLinear));
            }
        }

        public StepperLayout Layout
        {
            get
            {
                return m_layout;
            }

            set
            {
                m_layout = value;

                OnPropertyChanged(nameof(Layout));
            }
        }

        public IEnumerable<StepperLayout> Layouts
        {
            get
            {
                return new List<StepperLayout>()
                {
                    StepperLayout.Horizontal,
                    StepperLayout.Vertical
                };
            }
        }

        public StepperBaseViewModel()
            : base()
        {
            m_layout = StepperLayout.Vertical;
            m_isLinear = true;
            m_contentAnimationsEnabled = true;
        }
    }
}
