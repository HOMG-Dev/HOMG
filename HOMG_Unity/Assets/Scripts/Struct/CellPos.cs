using UnityEngine;

[System.Serializable]
public class CellPos
{
    public int x;
    public int y;

    public CellPos()
    {
        x = 0;
        y = 0;
    }
    public CellPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // 重写 Equals 方法，用于比较 CellPos 对象是否相等
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        CellPos other = (CellPos)obj;
        return x == other.x && y == other.y;
    }

    // 重写 GetHashCode 方法，与 Equals 配合使用
    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    // 重写 ToString 方法，方便调试输出
    public override string ToString()
    {
        return $"{this.x}_{this.y}";
    }
}
