INSERT dbo.AspNetUsers
(
	Id,
    Name,
    SurName,
    Password,
    LockoutEndDateUtc,
    AccessFailedCount,
	EmailConfirmed,
    PhoneNumberConfirmed,
    TwoFactorEnabled,
	LockoutEnabled,
    UserName
)

VALUES
(   N'1',
    N'celal',       -- Name - nvarchar(max)
    N'demir',       -- SurName - nvarchar(max)
    N'123',       -- Password - nvarchar(max)
    GETDATE(), -- LockoutEndDateUtc - datetime
    0,         -- AccessFailedCount - int
	0,
	0,
	0,
	0,
    N'celal258'        -- UserName - nvarchar(256)
    )