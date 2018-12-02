using System;
using System.Web;

public class Session<T> where T : class
{
    private static readonly string _typeName = typeof(T).Name;
    public static T session;
    static Session()
    {
        if (HttpContext.Current.Session[_typeName] == null)
        {
            session = Activator.CreateInstance<T>();
            HttpContext.Current.Session[_typeName] = session;
        }
        else
            session = (T)HttpContext.Current.Session[_typeName];
    }

    public virtual void Reset()
    {
        // Xóa toàn bộ nội dung của session <T>
        // Phục hồi lại giá trị ban đầu cho session<T> 
    }

    public void Dispose()
    {
        session = null;
        HttpContext.Current.Session[_typeName] = null;
    }
}


