using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Practice1;

public partial class User
{
    private string _pwdHash = null;
    private static int _curUserId = -1;
    private const int _keySize = 64;
    private const int _iterations = 350000;
    private byte[] _salt = RandomNumberGenerator.GetBytes(_keySize);
    private HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public User() {}
    public User(User user)
    {
        Fio = user.Fio;
        Phone = user.Phone;
        Email = user.Email;
        PasswordHash = user.PasswordHash;
        BirthDate = user.BirthDate;
        RoleId = user.RoleId;
        GenderId = user.GenderId;
    }

    public int Id { get; set; }
    [Display(Name="ФИО")]
    public string Fio { get; set; } = null!;
    [Display(Name="Телефон")]
    public string Phone { get; set; } = null!;
    [Display(Name="Email")]
    public string Email { get; set; } = null!;
    [Display(Name="Пароль")]
    [DataType(DataType.Password)]
    public string PasswordHash
    {
        get{ return _pwdHash;}
        set{ _pwdHash =  HashPassword(value); } 
    }
    
    public string? PasswordSalt 
    {   get => Convert.ToHexString(_salt); 
        set => _salt = Convert.FromHexString(value);
    }
    
    [Display(Name="Дата рождения")]
    [DataType(DataType.Date)]
    //[DisplayFormat(DataFormatString = "dd-MM-yyyy")]
    [Range(1900, 2023, ErrorMessage = "Недопустимая дата")]
    public DateTime BirthDate { get; set; }
    [Display(Name="Опыт тренировок")]
    [Range(0,100, ErrorMessage = "Неверное значение")]
    public int TrainingExpereince { get; set; }
    [Display(Name="Спортивные достижения")]
    public string? SportAchivements { get; set; }
    [Display(Name="Особенности здоровья")]
    public string? HealthFeatures { get; set; }
    [Display(Name="Роль")]
    public int RoleId { get; set; }
    [Display(Name="Пол")]
    public int GenderId { get; set; }

    public virtual Gender Gender { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

    public static int CurrentUserIdGet() => _curUserId;
    public static void CurrentUserIdSet(int id) => _curUserId = id;
    
    public static bool IsAdmin()
    {
        var _context = new PracticeCrossfitContext();
        if (_curUserId == -1)
            return false;
        else if (_context.Users.FirstOrDefault(u => u.Id == _curUserId).RoleId == 1)
            return true;
        return false;
    }
    
    public static bool IsUser()
    {
        var _context = new PracticeCrossfitContext();
        if (_curUserId == -1)
            return false;
        else if (_context.Users.FirstOrDefault(u => u.Id == _curUserId).RoleId > -1)
            return true;
        return false;
    }
    
    public async Task CreateAsync(User user)=> new User(user);
  
    
    private string HashPassword(string password)
    {
        // _pwdHash = Rfc2898DeriveBytes.Pbkdf2(
        //     password,
        //     _salt,
        //     _iterations,
        //     _hashAlgorithm,
        //     _keySize);
        return password;
    }
    
    public bool VerifyPassword(string password)
    {
        // var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
        //     password,
        //     _salt,
        //     _iterations,
        //     _hashAlgorithm, 
        //     _keySize);
        // var res= CryptographicOperations.FixedTimeEquals(_pwdHash,hashToCompare);
        return _pwdHash==password;
    }
}
