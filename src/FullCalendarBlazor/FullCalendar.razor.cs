using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Display;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.International;
using FullCalendarBlazor.Models.Views;
using FullCalendarBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        private readonly Dictionary<PropertyInfo, string> _calendarProperties;
        private readonly Dictionary<PropertyInfo, (string jsName, string dotnetName)> _calendarMethods;

        public FullCalendar()
        {
            _calendarProperties = new Dictionary<PropertyInfo, string>();
            _calendarMethods = new Dictionary<PropertyInfo, (string, string)>();

            foreach (var property in GetType().GetProperties())
            {
                if (property.Name.StartsWith("OnGet"))
                    _calendarMethods.Add(property, ($"{property.Name.Substring(5, 1).ToLower()}{property.Name.Substring(6)}", $"_{property.Name.Substring(2)}"));

                else if (property.Name.StartsWith("On"))
                    _calendarMethods.Add(property, ($"{property.Name.Substring(2, 1).ToLower()}{property.Name.Substring(3)}", $"_{property.Name.Substring(2)}"));

                else
                    _calendarProperties.Add(property, $"{property.Name.Substring(0, 1).ToLower()}{property.Name.Substring(1)}");
            }
        }

        // Injected Dependencies
        [Inject] private IJSRuntimeService RuntimeService { get; set; }
        [Inject] private IJSInvokableService InvokableService { get; set; }

        // Parameters

        [Parameter] public string Id { get; set; }

        #region Overall Display

        [Parameter] public Toolbar HeaderToolbar { get; set; }
        [Parameter] public bool? OmitHeaderToolbar { get; set; }
        [Parameter] public Toolbar FooterToolbar { get; set; }
        [Parameter] public bool? OmitFooterToolbar { get; set; }
        [Parameter] public DateTimeFormatter TitleFormat { get; set; }
        [Parameter] public string TitleRangeSeparator { get; set; }
        [Parameter] public Dictionary<string, string> ButtonText { get; set; }
        [Parameter] public Dictionary<string, string> ButtonIcons { get; set; }
        [Parameter] public Dictionary<string, CustomButton> CustomButtons { get; set; }
        [Parameter] public ThemeSystemOption? ThemeSystem { get; set; }
        // Todo: Add configuration for Bootstrap theming
        [Parameter] public string Height { get; set; }
        [Parameter] public string ContentHeight { get; set; }
        [Parameter] public double? AspectRatio { get; set; }
        [Parameter] public bool? ExpandRows { get; set; }
        [Parameter] public bool? HandleWindowResize { get; set; }
        [Parameter] public int? WindowResizeDelay { get; set; }
        [Parameter] public bool? StickyHeaderDates { get; set; }
        [Parameter] public bool? StickyFooterScrollbar { get; set; }
        [Parameter] public Action<WindowResizeInfo> OnWindowResize { get; set; }

        #endregion

        #region Views

        [Parameter] public bool? FixedWeekCount { get; set; }
        [Parameter] public bool? ShowNonCurrentDates { get; set; }
        [Parameter] public int? EventMinHeight { get; set; }
        [Parameter] public int? EventShortHeight { get; set; }
        [Parameter] public bool? SlotEventOverlap { get; set; }
        [Parameter] public bool? AllDaySlot { get; set; }
        // Todo: Add OnAllDayClassNames EventCallback
        // Todo: Add OnAllDayContent EventCallback
        [Parameter] public Action<AllDayInfo> OnAllDayDidMount { get; set; }
        [Parameter] public Action<AllDayInfo> OnAllDayWillUnmount { get; set; }
        [Parameter] public DateTimeFormatter ListDayFormat { get; set; }
        [Parameter] public bool? OmitListDayFormat { get; set; }
        [Parameter] public DateTimeFormatter ListDaySideFormat { get; set; }
        [Parameter] public bool? OmitListDaySideFormat { get; set; }
        // Todo: Add OnNoEventsClassNames EventCallback
        // Todo: Add OnNoEventsContent EventCallback
        [Parameter] public Action<NoEventsInfo> OnNoEventsDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<NoEventsInfo> OnNoEventsWillUnmount { get; set; } // Todo: Replace object with proper type
        [Parameter] public TimeSpan? Duration { get; set; }
        [Parameter] public int? DayCount { get; set; }
        [Parameter] public DateRange VisibleRange { get; set; }
        [Parameter] public Func<DateTime, DateRange> OnGetVisibleRange { get; set; }
        [Parameter] public string InitialView { get; set; }
        [Parameter] public Dictionary<string, object> Views { get; set; }
        // Todo: Add OnViewClassNames EventCallback
        [Parameter] public Action<ViewInfo> OnViewDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<ViewInfo> OnViewWillUnmount { get; set; } // Todo: Replace object with proper type

        #endregion

        #region Date and Time

        [Parameter] public bool? Weekends { get; set; }
        [Parameter] public IEnumerable<DayOfWeek> HiddenDays { get; set; }
        [Parameter] public bool? DayHeaders { get; set; }
        [Parameter] public DateTimeFormatter DayHeaderFormat { get; set; }
        [Parameter] public int? DayMinWidth { get; set; }
        // Todo: OnDayHeaderClassNames
        // Todo: OnDayHeaderContent
        [Parameter] public Action<DayHeaderRenderInfo> OnDayHeaderDidMount { get; set; }
        [Parameter] public Action<DayHeaderRenderInfo> OnDayHeaderWillUnmount { get; set; }
        // Todo: OnDayCellClassNames
        // Todo: OnDayCellContent
        [Parameter] public Action<DayCellRenderInfo> OnDayCellDidMount { get; set; }
        [Parameter] public Action<DayCellRenderInfo> OnDayCellWillUnmount { get; set; }
        [Parameter] public TimeSpan? SlotDuration { get; set; }
        [Parameter] public TimeSpan? SlotLabelInterval { get; set; }
        [Parameter] public DateTimeFormatter SlotLabelFormat { get; set; }
        [Parameter] public TimeSpan? SlotMinTime { get; set; }
        [Parameter] public TimeSpan? SlotMaxTime { get; set; }
        [Parameter] public TimeSpan? ScrollTime { get; set; }
        [Parameter] public bool? ScrollTimeReset { get; set; }
        // Todo: OnSlotLabelClassNames
        // Todo: OnSlotLabelContent
        [Parameter] public Action<SlotRenderInfo> OnSlotLabelDidMount { get; set; }
        [Parameter] public Action<SlotRenderInfo> OnSlotLabelWillUnmount { get; set; }
        // Todo: OnSlotLaneClassNames
        // Todo: OnSlotLaneContent
        [Parameter] public Action<SlotRenderInfo> OnSlotLaneDidMount { get; set; }
        [Parameter] public Action<SlotRenderInfo> OnSlotLaneWillUnmount { get; set; }
        [Parameter] public Action<DateInfo> OnDatesSet { get; set; }
        [Parameter] public DateTime? InitialDate { get; set; }
        [Parameter] public TimeSpan? DateIncrement { get; set; }
        [Parameter] public DateAlignmentOption? DateAlignment { get; set; }
        [Parameter] public DateRange ValidRange { get; set; }
        [Parameter] public bool? NavLinks { get; set; }
        [Parameter] public string NavLinkDayClick { get; set; }
        [Parameter] public Action<DateTime, object> OnNavLinkDayClick { get; set; }
        [Parameter] public string NavLinkWeekClick { get; set; }
        [Parameter] public Action<DateTime, object> OnNavLinkWeekClick { get; set; }
        [Parameter] public bool? WeekNumbers { get; set; }
        [Parameter] public WeekNumberOption? WeekNumberCalculation { get; set; }
        [Parameter] public Func<DateTime, int> OnGetWeekNumber { get; set; }
        [Parameter] public string WeekText { get; set; }
        [Parameter] public DateTimeFormatter WeekNumberFormat { get; set; }
        // Todo: OnWeekNumberClassNames
        // Todo: OnWeekNumberContent
        [Parameter] public Action<WeekNumberInfo> OnWeekNumberDidMount { get; set; }
        [Parameter] public Action<WeekNumberInfo> OnWeekNumberWillUnmount { get; set; }
        [Parameter] public bool? Selectable { get; set; }
        [Parameter] public bool? SelectMirror { get; set; }
        [Parameter] public bool? UnselectAuto { get; set; }
        [Parameter] public string UnselectCancel { get; set; }
        [Parameter] public bool? SelectOverlap { get; set; }
        [Parameter] public Func<Event, bool> OnGetSelectOverlap { get; set; }
        [Parameter] public object SelectConstraint { get; set; }
        [Parameter] public Func<SelectInfo, bool> OnGetSelectAllow { get; set; }
        [Parameter] public int? SelectMinDistance { get; set; }
        [Parameter] public Action<DateClickInfo> OnDateClick { get; set; }
        [Parameter] public Action<SelectionInfo> OnSelect { get; set; }
        [Parameter] public Action<object, View> OnUnselect { get; set; } // Todo
        [Parameter] public bool? NowIndicator { get; set; }
        [Parameter] public DateTime? Now { get; set; }
        [Parameter] public Func<DateTime> OnGetNow { get; set; }
        // Todo: OnNowIndicatorClassNames
        // Todo: OnNowIndicatorContent
        [Parameter] public Action<NowIndicatorInfo> OnNowIndicatorDidMount { get; set; }
        [Parameter] public Action<NowIndicatorInfo> OnNowIndicatorWillUnmount { get; set; }
        [Parameter] public object BusinessHours { get; set; }

        #endregion

        #region Events

        [Parameter] public IEnumerable<Event> Events { get; set; }
        [Parameter] public Func<object, Event> OnGetEventDataTransform { get; set; }
        [Parameter] public bool? DefaultAllDay { get; set; }
        [Parameter] public TimeSpan? DefaultAllDayEventDuration { get; set; }
        [Parameter] public TimeSpan? DefaultTimedEventDuration { get; set; }
        [Parameter] public bool? ForceEventDuration { get; set; }
        [Parameter] public Action<EventAddInfo> OnEventAdd { get; set; }
        [Parameter] public Action<EventChangeInfo> OnEventChange { get; set; }
        [Parameter] public Action<EventAddInfo> OnEventRemove { get; set; }
        [Parameter] public Action<IEnumerable<Event>> OnEventsSet { get; set; }
        // Todo: Event sources
        [Parameter] public string EventColor { get; set; }
        [Parameter] public string EventBackgroundColor { get; set; }
        [Parameter] public string EventBorderColor { get; set; }
        [Parameter] public string EventTextColor { get; set; }
        [Parameter] public EventDisplayOption? EventDisplay { get; set; }
        [Parameter] public DateTimeFormatter EventTimeFormat { get; set; }
        [Parameter] public bool? DisplayEventTime { get; set; }
        [Parameter] public bool? DisplayEventEnd { get; set; }
        [Parameter] public TimeSpan? NextDayThreshold { get; set; }
        [Parameter] public IEnumerable<string> EventOrder { get; set; }
        [Parameter] public Func<Event, Event, int> OnGetEventOrder { get; set; }
        [Parameter] public bool? EventOrderStrict { get; set; }
        [Parameter] public bool? ProgressiveEventRendering { get; set; }
        // [Parameter] public Func<EventRenderInfo, IEnumerable<string>> OnEventClassNames { get; set; } // Todo
        // [Parameter] public Func<EventRenderInfo, object> OnEventContent { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<EventRenderInfo> OnEventDidMount { get; set; }
        [Parameter] public Action<EventRenderInfo> OnEventWillUnmount { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventClick { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseEnter { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseLeave { get; set; }
        [Parameter] public bool? Editable { get; set; }
        [Parameter] public bool? EventStartEditable { get; set; }
        [Parameter] public bool? EventResizableFromStart { get; set; }
        [Parameter] public bool? EventDurationEditable { get; set; }
        [Parameter] public bool? EventResourceEditable { get; set; }
        [Parameter] public bool? Droppable { get; set; }
        [Parameter] public int? EventDragMinDistance { get; set; }
        [Parameter] public int? DragRevertDuration { get; set; }
        [Parameter] public bool? DragScroll { get; set; }
        [Parameter] public TimeSpan? SnapDuration { get; set; }
        [Parameter] public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent parameter (https://fullcalendar.io/docs/fixedMirrorParent)
        [Parameter] public bool? EventOverlap { get; set; }
        [Parameter] public Func<Event, Event, bool> OnGetEventOverlap { get; set; }
        [Parameter] public object EventConstraint { get; set; }
        [Parameter] public Func<EventAllowInfo, Event, bool> OnGetEventAllow { get; set; }
        [Parameter] public string DropAccept { get; set; }
        [Parameter] public Func<object, bool> OnGetDropAccept { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventDragStart { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventDragStop { get; set; }
        [Parameter] public Action<EventDropInfo> OnEventDrop { get; set; }
        [Parameter] public Action<DropInfo> OnDrop { get; set; }
        [Parameter] public Action<ExternalEventDropInfo> OnEventReceive { get; set; }
        [Parameter] public Action<ExternalEventDropInfo> OnEventLeave { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventResizeStart { get; set; }
        [Parameter] public Action<EventDragInfo> OnEventResizeStop { get; set; }
        [Parameter] public Action<EventResizeInfo> OnEventResize { get; set; }
        [Parameter] public int? DayMaxEventRows { get; set; }
        [Parameter] public bool? LimitDayEventRowsToHeight { get; set; }
        [Parameter] public int? DayMaxEvents { get; set; }
        [Parameter] public bool? LimitDayEventsToHeight { get; set; }
        [Parameter] public int? EventMaxStack { get; set; }
        [Parameter] public string MoreLinkClick { get; set; }
        [Parameter] public Action<MoreLinkClickInfo> OnMoreLinkClick { get; set; }
        [Parameter] public DateTimeFormatter DayPopoverFormat { get; set; }
        // [Parameter] public Func<int, string, IEnumerable<string>> OnMoreLinkClassNames { get; set; } // Todo
        // [Parameter] public Func<int, string, object> OnMoreLinkContent { get; set; } // Todo: Replace object with proper type // Todo
        [Parameter] public Action<MoreLinkInfo> OnMoreLinkDidMount { get; set; }
        [Parameter] public Action<MoreLinkInfo> OnMoreLinkWillUnmount { get; set; }

        #endregion

        #region International

        [Parameter] public string Locale { get; set; }
        [Parameter] public DirectionOption? Direction { get; set; }
        [Parameter] public DayOfWeek? FirstDay { get; set; }
        [Parameter] public string TimeZone { get; set; }

        #endregion

        // Public methods

        #region Overall Display

        public async Task UpdateSizeAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "updateSize");

        #endregion

        #region Views

        public async Task<View> GetViewAsync() => await RuntimeService.GetPropertyAsync<View>(Id, "view");
        public async Task ChangeViewAsync(string viewOption) =>  await RuntimeService.ExecuteVoidMethodAsync(Id, "changeView", viewOption);
        public async Task ChangeViewAsync(string viewOption, DateTime date) =>  await RuntimeService.ExecuteVoidMethodAsync(Id, "changeView", viewOption, date);

        #endregion

        #region Date and Time

        public async Task SetScrollToTimeAsync(TimeSpan duration) => await RuntimeService.ExecuteVoidMethodAsync(Id, "scrollToTime", duration);
        public async Task GoToPrevAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "prev");
        public async Task GoToNextAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "next");
        public async Task GoToPrevYearAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "prevYear");
        public async Task GoToNextYearAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "nextYear");
        public async Task GoToTodayAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "today");
        public async Task GoToDateAsync(DateTime date) => await RuntimeService.ExecuteVoidMethodAsync(Id, "gotoDate", date);
        public async Task IncrementDateAsync(TimeSpan duration) => await RuntimeService.ExecuteVoidMethodAsync(Id, "incrementDate", duration);
        public async Task<DateTime> GetDateAsync() => await RuntimeService.ExecuteMethodAsync<DateTime>(Id, "getDate");
        public async Task SelectPeriodAsync(DateTime start) => await RuntimeService.ExecuteVoidMethodAsync(Id, "select", start);
        public async Task SelectPeriodAsync(DateTime start, DateTime end) => await RuntimeService.ExecuteVoidMethodAsync(Id, "select", start, end);
        public async Task SelectPeriodAsync(SelectInfo selectInfo) => await RuntimeService.ExecuteVoidMethodAsync(Id, "select", selectInfo); // Todo: All day prop

        public async Task UnselectPeriodAsync() => await RuntimeService.ExecuteVoidMethodAsync(Id, "unselect");

        #endregion

        #region Event

        public async Task<IEnumerable<Event>> GetEvents() => await RuntimeService.ExecuteMethodAsync<IEnumerable<Event>>(Id, "getEvents");
        public async Task<Event> GetEventById(string id) => await RuntimeService.ExecuteMethodAsync<Event>(Id, "getEventById", id);
        public async Task<Event> AddEvent(Event newEvent) => await RuntimeService.ExecuteMethodAsync<Event>(Id, "addEvent", newEvent); // Todo: sources
        public async Task SetEventProp(Event selectedEvent, string name, object value) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setProp", name, value);
        public async Task SetEventExtendedProp(Event selectedEvent, string name, object value) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setExtendedProp", name, value);
        public async Task SetEventStart(Event selectedEvent, DateTime date) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setStart", date);
        public async Task SetEventStart(Event selectedEvent, DateTime date, bool maintainDuration) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setStart", date, new { maintainDuration });
        public async Task SetEventEnd(Event selectedEvent, DateTime date) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setEnd", date);
        public async Task SetEventDates(Event selectedEvent, DateTime start, DateTime end) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setDates", start, end);
        public async Task SetEventDates(Event selectedEvent, DateTime start, DateTime end, bool allDay) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setDates", start, end, new { allDay });
        public async Task SetEventAllDay(Event selectedEvent, bool allDay) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setAllDay", allDay);
        public async Task SetEventAllDay(Event selectedEvent, bool allDay, bool maintainDuration) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setAllDay", allDay, new { maintainDuration });
        public async Task MoveEventStart(Event selectedEvent, TimeSpan delta) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "moveStart", delta);
        public async Task MoveEventEnd(Event selectedEvent, TimeSpan delta) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "moveEnd", delta);
        public async Task MoveEventDates(Event selectedEvent, TimeSpan delta) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "moveDates");
        public async Task FormatEventRange(Event selectedEvent, DateTimeFormatter formatConfig) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "formatRange", formatConfig);
        public async Task RemoveEvent(Event selectedEvent) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "remove");
        public async Task<IEnumerable<object>> GetEventResources(Event selectedEvent) => await RuntimeService.ExecuteEventMethodAsync<IEnumerable<object>>(Id, selectedEvent.Id, "getResources"); // todo
        public async Task SetEventResources(Event selectedEvent, IEnumerable<object> resources) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "setResources", resources); // todo
        public async Task EventToPlainObject(Event selectedEvent, bool collapseExtendedProps, bool collapseColor) => await RuntimeService.ExecuteVoidEventMethodAsync(Id, selectedEvent.Id, "toPlainObject", new { collapseExtendedProps, collapseColor });

        #endregion

        // Lifecycle methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var calendarData = new ExpandoObject() as IDictionary<string, object>;
            var calendarMethods = new List<(string, string)>();

            foreach (var (property, jsName) in _calendarProperties)
            {
                var value = property.GetValue(this);
                if (value != null)
                    calendarData.Add(jsName, value);
            }

            foreach (var (property, (jsName, dotnetName)) in _calendarMethods)
            {
                var value = property.GetValue(this);
                InvokableService.GetType().GetProperty(property.Name)?.SetValue(InvokableService, value);
                if (value != null)
                    calendarMethods.Add((jsName, dotnetName));
            }

            await RuntimeService.RenderAsync(calendarData, calendarMethods, DotNetObjectReference.Create(InvokableService));
        }
    }
}