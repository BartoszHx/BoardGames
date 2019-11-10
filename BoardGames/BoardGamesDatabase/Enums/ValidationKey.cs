
namespace BoardGameDatabase.Enums
{
    public enum ValidationKey
    {
        MatchNoUser,
        IsNull,
        MatchNoId,
        MatchNoJsonFormatGameData,
        MatchNullDateStart,
        MatchIncorectDateEnd,
        MatchNoUsers,
        PasswordNotCompare,
        NoPassword,
        PasswordTooShort,
        PasswordTooLong,
        PasswordHaveWhiteSpace,
        PasswordNoDigit,
        PasswordNoUpperSymbol,
        PasswordNoSpecialSymbol,
        PasswordChangeThisSame,
        PasswordRepeatNotThisSame,
        CantLogin,
        UserEmptyName,
        EmailIncorrect,
        EmailDuplicate,
        NullUser
    }
}
