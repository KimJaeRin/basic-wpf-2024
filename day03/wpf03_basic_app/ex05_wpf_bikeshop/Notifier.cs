﻿using System.ComponentModel;

namespace ex05_wpf_bikeshop
{
    public class Notifier : INotifyPropertyChanged
    {
        // 우리가 만드는 클래스의 속성값이 변경되면 변경되는것을 알려주는 이벤트 핸들러
        public event PropertyChangedEventHandler? PropertyChanged;
    
        // 프로퍼티가 변경됨

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
    }
}
