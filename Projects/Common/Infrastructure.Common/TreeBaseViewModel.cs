﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Infrastructure.Common
{
    public class TreeBaseViewModel<T> : BaseViewModel
        where T : TreeBaseViewModel<T>
    {
        public TreeBaseViewModel()
        {
            Children = new ObservableCollection<T>();
        }

        public ObservableCollection<T> Source { get; set; }

        bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;

                if (_isExpanded)
                    ExpandChildren(this as T);
                else
                    HideChildren(this as T);

                OnPropertyChanged("IsExpanded");
            }
        }

        void HideChildren(T parent)
        {
            foreach (T t in parent.Children)
            {
                if (Source.Contains(t))
                    Source.Remove(t);
                HideChildren(t);
            }
        }

        void ExpandChildren(T parent)
        {
            if (parent.IsExpanded)
            {
                int indexOf = Source.IndexOf(parent);
                for (int i = 0; i < parent.Children.Count; i++)
                {
                    if (Source.Contains(parent.Children[i]) == false)
                        Source.Insert(indexOf + 1 + i, parent.Children[i]);
                }

                foreach (T t in parent.Children)
                {
                    ExpandChildren(t);
                }
            }
        }

        public bool HasChildren
        {
            get { return (Children.Count > 0); }
        }

        public int Level
        {
            get { return GetAllParents().Count(); }
        }

        public void ExpantToThis()
        {
            GetAllParents().ForEach(x => x.IsExpanded = true);
        }

        List<T> GetAllParents()
        {
            if (Parent == null)
            {
                return new List<T>();
            }
            else
            {
                List<T> allParents = Parent.GetAllParents();
                allParents.Add(Parent);
                return allParents;
            }
        }

        public T Parent { get; set; }

        ObservableCollection<T> _children;
        public ObservableCollection<T> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public bool IsBold { get; set; }
    }
}