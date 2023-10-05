﻿using LinqToLdap;

namespace SipCallback.Ucm;

public class Ldap
{
	private const string ServerName = "192.168.28.10";
	private readonly IDirectoryContext _context;

	public Ldap(IDirectoryContext context)
	{
		_context = context;
	}

	public User? GetUser(int accountNumber)
	{
		return _context.Query<User>().FirstOrDefault(x => x.AccountNumber == accountNumber);
	}
}