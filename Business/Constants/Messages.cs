using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //public static string ProductAdded = "Ürün eklendi";
        //public static string ProductNameInvalid = "Ürün ismi geçersiz";
        //public static string MaintenanceTime = "Bakım yapılıyor";
        //public static string ProductsListed = "Ürünler listelendi";
        //public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün bulunabilir";
        //public static string ProductNameAlreadyExistError = "Aynı isimde zaten başka bir ürün var";
        //public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Bu işlem için yetkiniz bulunmamaktadır";

        public static string UserRegistered = "Kullanıcı kayıt edildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Yanlış şifre";
        public static string SuccessfulLogin = "Giriş yapıldı";

        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";

        public static string AuthorsListed = "Yazarlar listelendi";
        public static string AuthorNameAlreadyExist = "Bu yazar zaten mevcut";
        public static string AuthorAdded = "Yazar eklendi";
        public static string AuthorUpdated = "Yazar güncellendi";
        public static string AuthorDeleted = "Yazar silindi";
        public static string DeletedAuthorHasBooks = "Silinmek istenen yazarın kitapları var";
        public static string AuthorNameIsEmpty = "Yazar ismi boş bırakılamaz";
        public static string AuthorNameIsShort = "Yazar ismi en az 2 karakter olmalıdır";

        public static string BooksListed = "Kitaplar listelendi";
        public static string BookAdded = "Kitap eklendi";
        public static string BookUpdated = "Kitap güncellendi";
        public static string BookDeleted = "Kitap silindi";
        public static string BookAuthorIdIncorrect = "Geçersiz yazar id";
        public static string BookCategoryIdIncorrect = "Geçersiz kategori id";
        public static string BookNameAlreadyExist = "Bu kitap adı zaten mevcut";
        public static string ISBNAlreadyExist = "Bu ISBN zaten mevcut";
        public static string MostReadBooksListed = "En çok okunan kitaplar başarıyla listelendi.";
        public static string BookListLimitIsIncorrect = "Limit 1 ile 50 arasında olmalıdır.";

        public static string BorrowsListed = "Ödünç almalar listelendi";
        public static string BorrowUserIdIncorrect = "Geçersiz kullanıcı id";
        public static string BorrowBookIdIncorrect = "Geçersiz kitap id";
        public static string BorrowUserLimitExceed = "Kullanıcı ödünç alma limitine ulaştı";
        public static string BorrowBookHasNoStock = "Stokta kitap yok";
        public static string BorrowSuccessful = "Kitap ödünç verildi";
        public static string BorrowReturned = "Kitap geri alındı";

        public static string CategoryNameIsEmpty = "Kategori ismi boş bırakılamaz";
        public static string CategoryNameIsShort = "Kategori ismi en az 2 karakter olmalıdır";
        public static string CategoryNameAlreadyExist = "Bu kategori zaten mevcut";
        public static string CategoryAdded = "Kategori eklendi";
        public static string CategoryUpdated = "Kategori güncellendi";
        public static string CategoryDeleted = "Kategori silindi";
        public static string DeletedCategoryHasBooks = "Silinmek istenen kategorinin kitapları var";

        public static string InvalidBorrowDate = "Geçersiz ödünç tarihi";
        public static string ISBNIsEmpty = "ISBN boş bırakılamaz";
        public static string ISBNIsShort = "ISBN en az 10 karakter olmalıdır";
        public static string PageCount = "Sayfa sayısı geçersiz";
        public static string TitleIsEmpty = "Başlık boş bırakılamaz";
        public static string TitleIsShort = "Başlık en az 2 karakter olmalıdır";
    }
}