using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarControleer : MonoBehaviour
{
    public MousePushBall MousePushBall = new MousePushBall();
    [System.Serializable]
    public class DiemMoi
    {
        public string tenManChoi;
        public int diem;
    }
    public List<DiemMoi> danhSachDiem = new List<DiemMoi>(); // Danh s�ch ?i?m c?a t?ng m�n ch?i
    public string tenTepTinDiem = "DiemMoi.txt"; // T�n t?p tin ?? l?u ?i?m
    private void Start()
    {
        // ???ng d?n ??y ?? ??n t?p tin
        
    }

    // H�m ?? l?u ?i?m v�o t?p tin
    private void LuuDiemVaoTepTin(string duongDanTepTin)
    {
        using (StreamWriter writer = new StreamWriter(duongDanTepTin))
        {
            foreach (var diem in danhSachDiem)
            {
                writer.WriteLine(diem.tenManChoi + ": " + diem.diem);
            }
        }
    }
    private void Update()
    {
        if (MousePushBall.checkEnd)
        { 
            danhSachDiem[0].diem = MousePushBall.click;
            string duongDanDayDu = Path.Combine(Application.persistentDataPath, tenTepTinDiem);
            int number = SceneManager.GetActiveScene().buildIndex;
            danhSachDiem[number - 1].tenManChoi = "Level: " + SceneManager.GetActiveScene().buildIndex;
            if (File.Exists(duongDanDayDu))
            {
                // ??c ?i?m t? t?p tin (n?u t?p tin ?� t?n t?i)
                List<DiemMoi> diemTruocDo = new List<DiemMoi>();

                using (StreamReader reader = new StreamReader(duongDanDayDu))
                {
                    string duLieuDiem = reader.ReadLine();
                    while (!string.IsNullOrEmpty(duLieuDiem))
                    {
                        string[] phanTachDiem = duLieuDiem.Split(':');
                        if (phanTachDiem.Length == 2)
                        {
                            string tenManChoi = phanTachDiem[0].Trim();
                            int diem = int.Parse(phanTachDiem[1].Trim());
                            diemTruocDo.Add(new DiemMoi { tenManChoi = tenManChoi, diem = diem });
                        }

                        duLieuDiem = reader.ReadLine();
                    }
                }

                // C?p nh?t ?i?m c?a t?ng m�n ch?i
                foreach (var diem in diemTruocDo)
                {
                    var diemHienTai = danhSachDiem.Find(x => x.tenManChoi == diem.tenManChoi);
                    if (diemHienTai != null && diem.diem > diemHienTai.diem)
                    {
                        diemHienTai.diem = diem.diem;
                    }
                }

                // L?u l?i danh s�ch ?i?m c?p nh?t v�o t?p tin
                LuuDiemVaoTepTin(duongDanDayDu);
            }
            else
            {
                // N?u kh�ng c� t?p tin, l?u ?i?m ban ??u c?a t?ng m�n ch?i
                LuuDiemVaoTepTin(duongDanDayDu);
            }

            // Log ?i?m c?a t?ng m�n ch?i (?? ki?m tra)
            foreach (var diem in danhSachDiem)
            {
                Debug.Log("Diem cua " + diem.tenManChoi + ": " + diem.diem);
            }
        }
    }
}
