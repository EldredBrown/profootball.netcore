using System;
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
    public class TeamSeasonScheduleAveragesControllerTest
    {
        [Fact]
        public async Task GetTeamSeasonScheduleAverages_WhenExceptionIsCaught_ShouldReturnInternalServerError()
        {
            // Arrange
            var teamSeasonScheduleAveragesRepository = A.Fake<ITeamSeasonScheduleAveragesRepository>();
            A.CallTo(() => teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAveragesAsync(
                A<string>.Ignored, A<int>.Ignored)).Throws<Exception>();

            var mapper = A.Fake<IMapper>();

            var testController = new TeamSeasonScheduleAveragesController(teamSeasonScheduleAveragesRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleAverages(teamName, seasonYear);

            // Assert
            result.Result.ShouldBeOfType<ObjectResult>();
            ((ObjectResult)result.Result).StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
            ((ObjectResult)result.Result).Value.ShouldBe("Database failure");
        }

        [Fact]
        public async Task GetTeamSeasonScheduleAverages_WhenExceptionIsNotCaught_ShouldReturnTeamSeasonOpponentAveragesModel()
        {
            // Arrange
            var teamSeasonScheduleAveragesRepository = A.Fake<ITeamSeasonScheduleAveragesRepository>();
            TeamSeasonScheduleAverages? teamSeasonScheduleAverages = new TeamSeasonScheduleAverages();
            A.CallTo(() => teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAveragesAsync(
                A<string>.Ignored, A<int>.Ignored)).Returns(teamSeasonScheduleAverages);

            var mapper = A.Fake<IMapper>();
            var teamSeasonScheduleAveragesModel = new TeamSeasonScheduleAveragesModel();
            A.CallTo(() => mapper.Map<TeamSeasonScheduleAveragesModel>(A<TeamSeasonScheduleAverages>.Ignored))
                .Returns(teamSeasonScheduleAveragesModel);

            var testController = new TeamSeasonScheduleAveragesController(teamSeasonScheduleAveragesRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleAverages(teamName, seasonYear);

            // Assert
            A.CallTo(() => teamSeasonScheduleAveragesRepository.GetTeamSeasonScheduleAveragesAsync(teamName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<TeamSeasonScheduleAveragesModel>(teamSeasonScheduleAverages))
                .MustHaveHappenedOnceExactly();
            result.Value.ShouldBeOfType<TeamSeasonScheduleAveragesModel>();
        }
    }
}
