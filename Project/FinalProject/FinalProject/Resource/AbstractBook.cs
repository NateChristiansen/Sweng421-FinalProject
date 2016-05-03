﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace FinalProject
{
    [Serializable]
    public abstract class AbstractBook : IBook
    {
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public string Genre { get; protected set; }
        public string Publisher { get; protected set; }
        public decimal Price { get; protected set; }
        public BitmapImage Cover
        {
            get { return _cover; }
            protected set { _cover = value; }
        }
        [NonSerialized]
        private BitmapImage _cover;
        public string Summary { get; protected set; }
        public string Isbn { get; protected set; }

        public bool EqualsBook(IBook b)
        {
            return Title.Equals(b.Title);
        }
    }
}