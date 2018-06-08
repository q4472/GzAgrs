using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace Nskd
{
    public class NpcDataCell : INotifyPropertyChanged
    {
        private NpcDataColumn c;
        private Object v;
        public NpcDataCell()
        {
            c = null;
            v = null;
        }
        public NpcDataCell(NpcDataColumn column)
        {
            c = column;
            v = null;
        }
        public Object Value
        {
            get { return v; }
            set { setWithConverting(value); OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void setWithConverting(Object value)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                // null всегда можно сохранить не зависимо от типа колонки
                v = null;
            }
            else if ((c == null) || ((c != null) && (c.DataType == null)))
            {
                // если тип колонки не определён, то можно сохранить всё что угодно  
                v = value;
            }
            else if (value.GetType() == c.DataType)
            {
                // если совпадают типы сохраняемого значения и колонки, то можно сохранить
                v = value;
            }
            else
            {
                // пытаемся преобразовать к типу колонки
                switch (c.DataType.ToString())
                {
                    case "System.String":
                        v = System.Convert.ToString(value);
                        break;
                    case "System.DateTime":
                        v = System.Convert.ToDateTime(value);
                        break;
                    default:
                        break;
                }
                if (v == null)
                {
                    //throw new ArgumentException("Can't convert " + value.GetType() + " to " + c.DataType);
                }
            }
        }
    }

    // Типы ислользуемые для хранения данных
    public class NpcBoolean : INotifyPropertyChanged
    {
        private Boolean? v = null;
        public Boolean? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcByte : INotifyPropertyChanged
    {
        private Byte? v = null;
        public Byte? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcBytes : INotifyPropertyChanged
    {
        private Byte[] v = null;
        public Byte[] Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcChar : INotifyPropertyChanged
    {
        private Char? v = null;
        public Char? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcDateTime : INotifyPropertyChanged
    {
        private DateTime? v = null;
        public DateTime? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcDecimal : INotifyPropertyChanged
    {
        private Decimal? v = null;
        public Decimal? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcDouble : INotifyPropertyChanged
    {
        private Double? v = null;
        public Double? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcInt32 : INotifyPropertyChanged
    {
        private Int32? v = null;
        public Int32? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcInt64 : INotifyPropertyChanged
    {
        private Int64? v = null;
        public Int64? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcGuid : INotifyPropertyChanged
    {
        private Guid? v = null;
        public Guid? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcObject : INotifyPropertyChanged
    {
        private Object v = null;
        public Object Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcSingle : INotifyPropertyChanged
    {
        private Single? v = null;
        public Single? Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    public class NpcString : INotifyPropertyChanged
    {
        private String v = null;
        public String Value
        {
            get { return v; }
            set { v = value; OnPropertyChanged("Value"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class NpcDataRow
    {
        public NpcDataTable Table { get; set; }
        public Object[] ItemArray { get; set; }
        public NpcDataRow(NpcDataTable table)
        {
            Table = table;
            ItemArray = new Object[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                NpcDataColumn colunm = Table.Columns[i];
                switch (colunm.DataType.ToString())
                {
                    case "System.Boolean":
                    case "System.Nullable`1[System.Boolean]":
                        ItemArray[i] = new NpcBoolean();
                        break;
                    case "System.Byte":
                    case "System.Nullable`1[System.Byte]":
                        ItemArray[i] = new NpcByte();
                        break;
                    case "System.Byte[]":
                        ItemArray[i] = new NpcBytes();
                        break;
                    case "System.Char":
                    case "System.Nullable`1[System.Char]":
                        ItemArray[i] = new NpcChar();
                        break;
                    case "System.DateTime":
                    case "System.Nullable`1[System.DateTime]":
                        ItemArray[i] = new NpcDateTime();
                        break;
                    case "System.Decimal":
                    case "System.Nullable`1[System.Decimal]":
                        ItemArray[i] = new NpcDecimal();
                        break;
                    case "System.Double":
                    case "System.Nullable`1[System.Double]":
                        ItemArray[i] = new NpcDouble();
                        break;
                    case "System.Int32":
                    case "System.Nullable`1[System.Int32]":
                        ItemArray[i] = new NpcInt32();
                        break;
                    case "System.Int64":
                    case "System.Nullable`1[System.Int64]":
                        ItemArray[i] = new NpcInt64();
                        break;
                    case "System.Guid":
                    case "System.Nullable`1[System.Guid]":
                        ItemArray[i] = new NpcGuid();
                        break;
                    case "System.Object":
                        ItemArray[i] = new NpcObject();
                        break;
                    case "System.Single":
                    case "System.Nullable`1[System.Single]":
                        ItemArray[i] = new NpcSingle();
                        break;
                    case "System.String":
                        ItemArray[i] = new NpcString();
                        break;
                    default:
                        ItemArray[i] = new NpcObject();
                        break;
                }
            }
        }
        public Object this[Int32 index]
        {
            get
            {
                Object result = null;
                switch (ItemArray[index].GetType().ToString())
                {
                    case "Nskd.NpcBoolean":
                        result = ((NpcBoolean)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcByte":
                        result = ((NpcByte)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcBytes":
                        result = ((NpcBytes)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcChar":
                        result = ((NpcChar)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcDateTime":
                        result = ((NpcDateTime)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcDecimal":
                        result = ((NpcDecimal)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcDouble":
                        result = ((NpcDouble)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcInt32":
                        result = ((NpcInt32)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcInt64":
                        result = ((NpcInt64)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcGuid":
                        result = ((NpcGuid)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcObject":
                        result = ((NpcObject)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcSingle":
                        result = ((NpcSingle)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcString":
                        result = ((NpcString)ItemArray[index]).Value;
                        break;
                    case "Nskd.NpcDataCell":
                        result = ((NpcDataCell)ItemArray[index]).Value;
                        break;
                    default:
                        break;
                }
                return result;
            }
            set
            {
                switch (ItemArray[index].GetType().ToString())
                {
                    case "Nskd.NpcBoolean":
                        ((NpcBoolean)ItemArray[index]).Value = value as Boolean?;
                        break;
                    case "Nskd.NpcByte":
                        ((NpcByte)ItemArray[index]).Value = value as Byte?;
                        break;
                    case "Nskd.NpcBytes":
                        ((NpcBytes)ItemArray[index]).Value = value as Byte[];
                        break;
                    case "Nskd.NpcChar":
                        ((NpcChar)ItemArray[index]).Value = value as Char?;
                        break;
                    case "Nskd.NpcDateTime":
                        ((NpcDateTime)ItemArray[index]).Value = value as DateTime?;
                        break;
                    case "Nskd.NpcDecimal":
                        ((NpcDecimal)ItemArray[index]).Value = value as Decimal?;
                        break;
                    case "Nskd.NpcDouble":
                        ((NpcDouble)ItemArray[index]).Value = value as Double?;
                        break;
                    case "Nskd.NpcInt32":
                        ((NpcInt32)ItemArray[index]).Value = value as Int32?;
                        break;
                    case "Nskd.NpcInt64":
                        ((NpcInt64)ItemArray[index]).Value = value as Int64?;
                        break;
                    case "Nskd.NpcGuid":
                        ((NpcGuid)ItemArray[index]).Value = value as Guid?;
                        break;
                    case "Nskd.NpcObject":
                        ((NpcObject)ItemArray[index]).Value = value as Object;
                        break;
                    case "Nskd.NpcSingle":
                        ((NpcSingle)ItemArray[index]).Value = value as Single?;
                        break;
                    case "Nskd.NpcString":
                        ((NpcString)ItemArray[index]).Value = value as String;
                        break;
                    case "Nskd.NpcDataCell":
                        ((NpcDataCell)ItemArray[index]).Value = value;
                        break;
                    default:
                        ((NpcObject)ItemArray[index]).Value = value as Object;
                        break;
                }
            }
        }
        public Object this[String columnName]
        {
            get
            {
                Object result = null;
                for (int index = 0; index < Table.Columns.Count; index++)
                {
                    if (Table.Columns[index].ColumnName == columnName)
                    {
                        result = this[index];
                        break;
                    }
                }
                return result;
            }
            set
            {
                for (int index = 0; index < Table.Columns.Count; index++)
                {
                    if (Table.Columns[index].ColumnName == columnName)
                    {
                        this[index] = value;
                        break;
                    }
                }
            }
        }
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            foreach (NpcDataColumn column in Table.Columns)
            {
                String typeName = column.DataType.ToString();
                if ((typeName.Length > 18) &&
                    (typeName.Substring(0, 18) == "System.Nullable`1["))
                {
                    typeName = typeName.Substring(18, typeName.Length - 18 - 1);
                    dt.Columns.Add((String)column.ExtendedProperties["dbName"], Type.GetType(typeName));
                }
                else
                {
                    dt.Columns.Add((String)column.ExtendedProperties["dbName"], column.DataType);
                }
            }
            DataRow row = dt.NewRow();
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                NpcDataColumn colunm = Table.Columns[i];
                Object temp = this[i];
                row[i] = (temp == null) ? DBNull.Value : temp;
            }
            dt.Rows.Add(row);
            return dt;
        }
    }
    public class NpcDataRows : ObservableCollection<NpcDataRow> { }
    public class NpcDataColumn
    {
        public String ColumnName;
        public String Caption;
        public Type DataType;
        public PropertyCollection ExtendedProperties;
        public NpcDataColumn()
        {
            ColumnName = null;
            Caption = null;
            DataType = null;
            ExtendedProperties = new PropertyCollection();
        }
        public NpcDataColumn(String columnName)
        {
            ColumnName = columnName;
            Caption = null;
            DataType = null;
            ExtendedProperties = new PropertyCollection();
        }
        public NpcDataColumn(String columnName, Type dataType)
        {
            ColumnName = columnName;
            Caption = null;
            DataType = dataType;
            ExtendedProperties = new PropertyCollection();
        }
    }
    public class NpcDataColumns : Collection<NpcDataColumn> { }
    public class NpcDataTable
    {
        public NpcDataColumns Columns;
        public NpcDataRows Rows;
        public NpcDataTable()
        {
            Columns = new NpcDataColumns();
            Rows = new NpcDataRows();
        }
        public NpcDataRow NewRow()
        {
            NpcDataRow row = new NpcDataRow(this);
            return row;
        }
    }

    public class NpcDataView
    {
        public NpcDataColumns Columns;
        public NpcDataRows Rows;
        public String RowFilter;
        public NpcDataView(NpcDataTable table, String rowFilter)
        {
            Columns = table.Columns;
            RowFilter = rowFilter;
            Rows = new NpcDataRows();
            foreach (NpcDataRow oldRow in table.Rows)
            {
                if (((String)oldRow[1]).IndexOf(rowFilter) >= 0)
                {
                    NpcDataRow newRow = new NpcDataRow(table);
                    Rows.Add(newRow);
                    for (int i = 0; i < Columns.Count; i++)
                    {
                        newRow[i] = oldRow[i];
                    }
                }
            }
        }
    }
}
