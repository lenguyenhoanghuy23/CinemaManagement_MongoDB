using AutoMapper;
using CinemaManagement.CinemaZooms;
using CinemaManagement.Countries;
using CinemaManagement.Genres;
using CinemaManagement.Movies;
using CinemaManagement.ScreenTypes;
using CinemaManagement.ShowTimes;
using CinemaManagement.Tickets;
using MongoDB.Bson;

namespace CinemaManagement;

public class CinemaManagementApplicationAutoMapperProfile : Profile
{
    public CinemaManagementApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */


        //  screenType 
        CreateMap<screenType, screenTypeDto>();
        CreateMap<screenTypeCreateDto, screenType>();
        CreateMap<screenTypeUpdateDto, screenType>();
        // countrie
        CreateMap<countrie, countrieDto>();
        CreateMap<countrieCreateDto, countrie>();
        CreateMap<countrieUpdateDto, countrie>();
        // genre 
        CreateMap<genre, genreDto>();
        CreateMap<genreCreateDto, genre>();
        CreateMap<genreUpdateDto, genre>();
        /// cinemazoom
        CreateMap<cinemaZoom, cinemaZoomDto>();
        CreateMap<cinemaZoonCreateDto, cinemaZoom>();
        CreateMap<cinemaZoomUpdateDto, cinemaZoom>();
        // movie

        CreateMap<movie, movieDto>();
        CreateMap<movieDto, movie>();
        CreateMap<movieCreateDto, movie>();
        CreateMap<movieUpdateDto, movie>();
        //show time
        CreateMap<showTime, showTimeDto>();
        CreateMap<showTimeCreateDto, showTime>();
        CreateMap<showTimeUpdateDto, showTime>();

        //show time
        CreateMap<ticket, ticketDto>();
        CreateMap<ticketCreateDto, ticket>();

    }
}
