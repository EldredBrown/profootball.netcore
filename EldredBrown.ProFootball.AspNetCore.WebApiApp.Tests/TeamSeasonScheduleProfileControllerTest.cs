using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Controllers;
using EldredBrown.ProFootball.AspNetCore.WebApiApp.Models;
using EldredBrown.ProFootball.NETCore.Data.Entities;
using EldredBrown.ProFootball.NETCore.Data.Repositories;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace EldredBrown.ProFootball.AspNetCore.WebApiApp.Tests
{
    public class TeamSeasonScheduleProfileControllerTest
    {
        [Fact]
        public async Task GetTeamSeasonScheduleProfile_WhenExceptionIsCaught_ShouldReturnInternalServerError()
        {
            // Arrange
            var teamSeasonScheduleProfileRepository = A.Fake<ITeamSeasonScheduleProfileRepository>();
            A.CallTo(() => teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfileAsync(
                A<string>.Ignored, A<int>.Ignored)).Throws<Exception>();

            var mapper = A.Fake<IMapper>();

            var testController = new TeamSeasonScheduleProfileController(teamSeasonScheduleProfileRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleProfile(teamName, seasonYear);

            // Assert
            result.Result.ShouldBeOfType<ObjectResult>();
            ((ObjectResult)result.Result).StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
            ((ObjectResult)result.Result).Value.ShouldBe("Database failure");
        }

        [Fact]
        public async Task GetTeamSeasonScheduleProfile_WhenProfileIsEmpty_ShouldReturnNotFoundResult()
        {
            // Arrange
            var teamSeasonScheduleProfileRepository = A.Fake<ITeamSeasonScheduleProfileRepository>();
            var teamSeasonScheduleProfile = new List<TeamSeasonOpponentProfile>();
            A.CallTo(() => teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfileAsync(
                A<string>.Ignored, A<int>.Ignored)).Returns(teamSeasonScheduleProfile);

            var mapper = A.Fake<IMapper>();

            var testController = new TeamSeasonScheduleProfileController(teamSeasonScheduleProfileRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleProfile(teamName, seasonYear);

            // Assert
            A.CallTo(() => teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfileAsync(teamName, seasonYear))
                .MustHaveHappenedOnceExactly();
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetTeamSeasonScheduleProfile_WhenProfileIsNotEmpty_ShouldReturnTeamSeasonOpponentProfileModelArray()
        {
            // Arrange
            var teamSeasonScheduleProfileRepository = A.Fake<ITeamSeasonScheduleProfileRepository>();
            var teamSeasonScheduleProfile = new List<TeamSeasonOpponentProfile>
            {
                new TeamSeasonOpponentProfile()
            };
            A.CallTo(() => teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfileAsync(
                A<string>.Ignored, A<int>.Ignored)).Returns(teamSeasonScheduleProfile);

            var mapper = A.Fake<IMapper>();
            TeamSeasonOpponentProfileModel [] teamSeasonScheduleProfileModels = {
                new TeamSeasonOpponentProfileModel()
            };
            A.CallTo(() => mapper.Map<TeamSeasonOpponentProfileModel[]>(A<TeamSeasonOpponentProfile>.Ignored))
                .Returns(teamSeasonScheduleProfileModels);

            var testController = new TeamSeasonScheduleProfileController(teamSeasonScheduleProfileRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleProfile(teamName, seasonYear);

            // Assert
            A.CallTo(() => teamSeasonScheduleProfileRepository.GetTeamSeasonScheduleProfileAsync(teamName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<TeamSeasonOpponentProfileModel[]>(teamSeasonScheduleProfile))
                .MustHaveHappenedOnceExactly();
            result.Value.ShouldBeOfType<TeamSeasonOpponentProfileModel[]>();
        }
    }
}
