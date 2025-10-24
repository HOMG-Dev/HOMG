public class TupleCellPos
{
    public CellPos st;
    public CellPos ed;

    public TupleCellPos(CellPos st, CellPos ed)
    {
        this.st = st;
        this.ed = ed;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        TupleCellPos other = (TupleCellPos)obj;
        return st.Equals(other.st) && ed.Equals(other.ed);
    }

    public override int GetHashCode()
    {
        return st.GetHashCode() ^ ed.GetHashCode();
    }

    public override string ToString()
    {
        return $"{this.st}->{this.ed}";
    }
}

