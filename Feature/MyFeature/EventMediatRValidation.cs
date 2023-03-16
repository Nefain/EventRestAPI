using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using static MyFeature.Feature.MyFeature.EventMediatRValidation;
using ValidationException = FluentValidation.ValidationException;

namespace MyFeature.Feature.MyFeature
{
    public class EventMediatRValidation
    {
        public class Command : IRequest<bool>
        {
            public bool AddEvent { get; set; }
            public bool ChangeEvent { get; set; }
            public bool DeleteEvent { get; set; }
            public Events chgEvent { get; set; }
            public Guid eveId { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IValidator<Events> _validator;
            private readonly EventService _myEventService;

            public Handler(IValidator<Events> validator, EventService eventService)
            {
                _validator = validator;
                _myEventService = eventService;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                if (command.AddEvent)
                {
                    var validationResults = await new EventsValidator().ValidateAsync(command.chgEvent);
                    if (!validationResults.IsValid)
                    {
                        throw new ValidationException(validationResults.Errors);
                    }
                    _myEventService.AddEvent(command.chgEvent); // выполнение операции добавления события
                    return true;
                }

                if (command.ChangeEvent)
                {
                    _myEventService.UpdateEvent(command.eveId, command.chgEvent);
                    return true;
                }

                if (command.DeleteEvent)
                {
                    _myEventService.DeleteEvent(command.eveId);
                    return true;
                }

                return true;
            }
        }

    }

    public class GetEvent
    {
        public class Request : IRequest<Events>
        {
            public Guid eventId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Events>
        {
            private readonly EventService _eventService;

            public Handler(EventService eventService)
            {
                _eventService = eventService;
            }

            public async Task<Events> Handle(Request request, CancellationToken cancellationToken)
            {
                return _eventService.GetEvent(request.eventId);
            }
        }
    }

    public class GetAllEvent
    {
        public class Request : IRequest<IEnumerable<Events>>
        {

        }

        public class Handler : IRequestHandler<Request, IEnumerable<Events>>
        {
            private readonly  EventService _eventService;

            public Handler(EventService eventService)
            {
                _eventService = eventService;
            }

            public async Task<IEnumerable<Events>> Handle(Request request, CancellationToken cancellationToken)
            {
                return _eventService.GetAllEvents();
            }
        }
    }

    public class EventsValidator : AbstractValidator<Events>
    { 
        public EventsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

            RuleFor(c => c.idEvent)
                .NotEmpty().NotNull().WithMessage(msg);
            RuleFor(c => c.Name)
                .Must(c => c.All(Char.IsLetter)).WithMessage(msg);
            RuleFor(c => c.Description)
                .Must(c => c.All(Char.IsLetter)).WithMessage(msg);
            RuleFor(c => c.BeginTime)
                .NotEmpty().GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage(msg);
            RuleFor(c => c.EndTime)
                .NotEmpty().GreaterThan(c => c.BeginTime).WithMessage(msg);
            RuleFor(c => c.Imgid)
                .NotNull().NotEmpty().WithMessage(msg);
            RuleFor(c => c.Spaceid)
                .NotNull().NotEmpty().WithMessage(msg);
        }

    }
}
