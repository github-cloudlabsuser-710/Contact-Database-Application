using NUnit.Framework;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

[TestFixture]
public class UserControllerTests
{
    private UserController _controller;
    private List<User> _users;

    [SetUp]
    public void Setup()
    {
        _users = new List<User>
        {
            new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
            new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" }
        };

        UserController.userlist = _users;

        _controller = new UserController();
    }

    [Test]
    public void Index_ReturnsCorrectViewWithUsers()
    {
        var result = _controller.Index() as ViewResult;

        Assert.IsNotNull(result);
        var model = result.Model as List<User>;
        Assert.AreEqual(_users.Count, model.Count);
    }

    [Test]
    public void Details_ReturnsCorrectViewWithUser()
    {
        var result = _controller.Details(1) as ViewResult;

        Assert.IsNotNull(result);
        var model = result.Model as User;
        Assert.AreEqual(_users[0].Id, model.Id);
    }

    [Test]
    public void Create_AddsUserAndRedirects()
    {
        var newUser = new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" };

        var result = _controller.Create(newUser) as RedirectToRouteResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.RouteValues["action"]);
        Assert.AreEqual(3, UserController.userlist.Count);
    }

    [Test]
    public void Edit_UpdatesUserAndRedirects()
    {
        var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };

        var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.RouteValues["action"]);
        Assert.AreEqual("Updated User", UserController.userlist.First(u => u.Id == 1).Name);
    }

    [Test]
    public void Delete_RemovesUserAndRedirects()
    {
        var result = _controller.Delete(1, new FormCollection()) as RedirectToRouteResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.RouteValues["action"]);
        Assert.AreEqual(1, UserController.userlist.Count);
    }
}
