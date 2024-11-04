namespace Ledbim.ApiExample.Application.Helpers
{
    public abstract class ResultMessages
    {
        public static string Success => "İşlem Başarılı";
        public static string Fail => "İşlem Başarısız";


        #region Users
        public static string UserAlreadyCreated => "Bu Mail Adresi ile Sistemde Kayıtlı Kulanıcı Mevcut";
        public static string LoginFailed => "Kullanıcı Adı Şifre Yanlış. Giriş İşlemi Başarısız";
        public static string LoginSuccess => "Sisteme Giriş Başarılı";
        public static string UserNotFound => "Kullanıcı Bulunamadı";
        public static string UserPasswordNotCorrect => "Geçerli Şifrenizi Yanlış Girdiniz";
        public static string UserPasswordNotConfirmed => "Yeni Şifre İle Şifre Tekrarı Eşleşmiyor";
        public static string ResetPasswordMailSend => "Şifre Yenileme İletiniz E-Posta Adresinize Gönderilmiştir";
        #endregion
    }
}
