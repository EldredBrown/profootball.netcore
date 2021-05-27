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
    public class TeamSeasonScheduleTotalsControllerTest
    {
        [Fact]
        public async Task GetTeamSeasonScheduleTotals_WhenExceptionIsCaught_ShouldReturnInternalServerError()
        {
            // Arrange
            var teamSeasonScheduleTotalsRepository = A.Fake<ITeamSeasonScheduleTotalsRepository>();
            A.CallTo(() => teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotalsAsync(
                A<string>.Ignored, A<int>.Ignored)).Throws<Exception>();

            var mapper = A.Fake<IMapper>();

            var testController = new TeamSeasonScheduleTotalsController(teamSeasonScheduleTotalsRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleTotals(teamName, seasonYear);

            // Assert
            result.Result.ShouldBeOfType<ObjectResult>();
            ((ObjectResult)result.Result).StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
            ((ObjectResult)result.Result).Value.ShouldBe("Database failure");
        }

        [Fact]
        public async Task GetTeamSeasonScheduleTotals_WhenExceptionIsNotCaught_ShouldReturnTeamSeasonOpponentTotalsModel()
        {
            // Arrange
            var teamSeasonScheduleTotalsRepository = A.Fake<ITeamSeasonScheduleTotalsRepository>();
            TeamSeasonScheduleTotals? teamSeasonScheduleTotals = new TeamSeasonScheduleTotals();
            A.CallTo(() => teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotalsAsync(
                A<string>.Ignored, A<int>.Ignored)).Returns(teamSeasonScheduleTotals);

            var mapper = A.Fake<IMapper>();
            var teamSeasonScheduleTotalsModel = new TeamSeasonScheduleTotalsModel();
            A.CallTo(() => mapper.Map<TeamSeasonScheduleTotalsModel>(A<TeamSeasonScheduleTotals>.Ignored))
                .Returns(teamSeasonScheduleTotalsModel);

            var testController = new TeamSeasonScheduleTotalsController(teamSeasonScheduleTotalsRepository, mapper);

            string teamName = "Team";
            int seasonYear = 1920;

            // Act
            var result = await testController.GetTeamSeasonScheduleTotals(teamName, seasonYear);

            // Assert
            A.CallTo(() => teamSeasonScheduleTotalsRepository.GetTeamSeasonScheduleTotalsAsync(teamName, seasonYear))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => mapper.Map<TeamSeasonScheduleTotalsModel>(teamSeasonScheduleTotals))
                .MustHaveHappenedOnceExactly();
            result.Value.ShouldBeOfType<TeamSeasonScheduleTotalsModel>();
        }
    }
}
