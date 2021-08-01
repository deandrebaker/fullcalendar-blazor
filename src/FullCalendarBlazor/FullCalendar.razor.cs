using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullCalendarBlazor.Models;
using FullCalendarBlazor.Models.DateAndTime;
using FullCalendarBlazor.Models.Display;
using FullCalendarBlazor.Models.Events;
using FullCalendarBlazor.Models.Views;
using FullCalendarBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FullCalendarBlazor
{
    public partial class FullCalendar
    {
        // Injected Dependencies
        [Inject] private IJSRuntimeService JsInterop { get; set; }

        // Parameters

        [Parameter] public string Id { get; set; }

        #region Overall Display

        [Parameter] public Toolbar HeaderToolbar { get; set; }
        [Parameter] public Toolbar FooterToolbar { get; set; }
        [Parameter] public DateTimeFormatter TitleFormat { get; set; }
        [Parameter] public string TitleRangeSeparator { get; set; }
        [Parameter] public Dictionary<string, string> ButtonText { get; set; }
        [Parameter] public Dictionary<string, string> ButtonIcons { get; set; }
        [Parameter] public object CustomButtons { get; set; }
        [Parameter] public string ThemeSystem { get; set; }
        [Parameter] public string Height { get; set; }
        [Parameter] public string ContentHeight { get; set; }
        [Parameter] public double? AspectRatio { get; set; }
        [Parameter] public bool? ExpandRows { get; set; }
        [Parameter] public bool? HandleWindowResize { get; set; }
        [Parameter] public int? WindowResizeDelay { get; set; }
        [Parameter] public string StickyHeaderDates { get; set; }
        [Parameter] public string StickyFooterScrollbar { get; set; }
        [Parameter] public Action<View> OnWindowResize { get; set; }

        #endregion

        #region Views

        [Parameter] public bool? FixedWeekCount { get; set; }
        [Parameter] public bool? ShowNonCurrentDates { get; set; }
        [Parameter] public int? EventMinHeight { get; set; }
        [Parameter] public int? EventShortHeight { get; set; }
        [Parameter] public bool? SlotEventOverlap { get; set; }
        [Parameter] public bool? AllDaySlot { get; set; }
        // [Parameter] public Func<string, IEnumerable<string>> OnAllDayClassNames { get; set; } // Todo
        // [Parameter] public Func<string, object> OnAllDayContent { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<string> OnAllDayDidMount { get; set; }
        [Parameter] public Action<string> OnAllDayWillUnmount { get; set; }
        [Parameter] public DateTimeFormatter ListDayFormat { get; set; }
        [Parameter] public DateTimeFormatter ListDaySideFormat { get; set; }
        // [Parameter] public Func<object, IEnumerable<string>> OnNoEventsClassNames { get; set; } // Todo
        // [Parameter] public Func<object, object> OnNoEventsContent { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<object> OnNoEventsDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<object> OnNoEventsWillUnmount { get; set; } // Todo: Replace object with proper type
        [Parameter] public string InitialView { get; set; }
        // [Parameter] public Func<object, IEnumerable<string>> OnViewClassNames { get; set; } // Todo
        [Parameter] public Action<View, object> OnViewDidMount { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<View, object> OnViewWillUnmount { get; set; } // Todo: Replace object with proper type

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
        [Parameter] public string DateAlignment { get; set; }
        [Parameter] public DateRange ValidRange { get; set; }
        [Parameter] public bool? NavLinks { get; set; }
        [Parameter] public string NavLinkDayClick { get; set; }
        [Parameter] public string NavLinkWeekClick { get; set; }
        [Parameter] public bool? WeekNumbers { get; set; }
        [Parameter] public string WeekNumberCalculation { get; set; }
        [Parameter] public string WeekText { get; set; }
        [Parameter] public DateTimeFormatter WeekNumberFormat { get; set; }
        // Todo: OnWeekNumberClassNames
        // Todo: OnWeekNumberContent
        [Parameter] public Action<string, string, DateTime> OnWeekNumberDidMount { get; set; }
        [Parameter] public Action<string, string, DateTime> OnWeekNumberWillUnmount { get; set; }
        [Parameter] public bool? Selectable { get; set; }
        [Parameter] public bool? SelectMirror { get; set; }
        [Parameter] public bool? UnselectAuto { get; set; }
        [Parameter] public string UnselectCancel { get; set; }
        [Parameter] public bool? SelectOverlap { get; set; }
        [Parameter] public object SelectConstraint { get; set; }
        [Parameter] public Func<SelectInfo, bool> OnSelectAllow { get; set; }
        [Parameter] public int? SelectMinDistance { get; set; }
        [Parameter] public bool? NowIndicator { get; set; }
        [Parameter] public DateTime? Now { get; set; }
        // Todo: OnNowIndicatorClassNames
        // Todo: OnNowIndicatorContent
        [Parameter] public Action<NowIndicatorInfo> OnNowIndicatorDidMount { get; set; }
        [Parameter] public Action<NowIndicatorInfo> OnNowIndicatorWillUnmount { get; set; }
        [Parameter] public object BusinessHours { get; set; }

        #endregion

        #region Events

        [Parameter] public IEnumerable<Event> Events { get; set; }
        // Todo: Add EventDataTransform delegate for transforming events from a source (https://fullcalendar.io/docs/eventDataTransform)
        [Parameter] public bool? DefaultAllDay { get; set; }
        [Parameter] public TimeSpan? DefaultAllDayEventDuration { get; set; }
        [Parameter] public TimeSpan? DefaultTimedEventDuration { get; set; }
        [Parameter] public bool? ForceEventDuration { get; set; }
        [Parameter] public Action<EventAddInfo> OnEventAdd { get; set; }
        [Parameter] public Action<EventChangeInfo> OnEventChange { get; set; }
        [Parameter] public Action<EventAddInfo> OnEventRemove { get; set; }
        [Parameter] public Action<IEnumerable<Event>> OnEventsSet { get; set; }
        [Parameter] public string EventColor { get; set; }
        [Parameter] public string EventBackgroundColor { get; set; }
        [Parameter] public string EventBorderColor { get; set; }
        [Parameter] public string EventTextColor { get; set; }
        [Parameter] public string EventDisplay { get; set; }
        [Parameter] public DateTimeFormatter EventTimeFormat { get; set; }
        [Parameter] public bool? DisplayEventTime { get; set; }
        [Parameter] public bool? DisplayEventEnd { get; set; }
        [Parameter] public TimeSpan? NextDayThreshold { get; set; }
        [Parameter] public IEnumerable<string> EventOrder { get; set; }
        [Parameter] public bool? EventOrderStrict { get; set; }
        [Parameter] public bool? ProgressiveEventRendering { get; set; }
        // [Parameter] public Func<EventRenderInfo, IEnumerable<string>> OnEventClassNames { get; set; } // Todo
        // [Parameter] public Func<EventRenderInfo, object> OnEventContent { get; set; } // Todo: Replace object with proper type
        [Parameter] public Action<EventRenderInfo> OnEventDidMount { get; set; }
        [Parameter] public Action<EventRenderInfo> OnEventWillUnmount { get; set; }
        [Parameter] public bool? Editable { get; set; }
        [Parameter] public bool? EventStartEditable { get; set; }
        [Parameter] public bool? EventResizableFromStart { get; set; }
        [Parameter] public bool? EventDurationEditable { get; set; }
        [Parameter] public bool? EventResourceEditable { get; set; }
        [Parameter] public bool? Droppable { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventClick { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseEnter { get; set; }
        [Parameter] public Action<EventClickInfo> OnEventMouseLeave { get; set; }
        [Parameter] public int? EventDragMinDistance { get; set; }
        [Parameter] public int? DragRevertDuration { get; set; }
        [Parameter] public bool? DragScroll { get; set; }
        [Parameter] public TimeSpan? SnapDuration { get; set; }
        [Parameter] public bool? AllDayMaintainDuration { get; set; }
        // Todo: Add FixedMirrorParent parameter (https://fullcalendar.io/docs/fixedMirrorParent)
        [Parameter] public Func<Event, Event, bool> OnEventOverlap { get; set; }
        [Parameter] public object EventConstraint { get; set; }
        [Parameter] public Func<EventAllowInfo, Event, bool> OnEventAllow { get; set; }
        [Parameter] public Func<object, bool> OnDropAccept { get; set; }
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
        [Parameter] public int? DayMaxEvents { get; set; }
        [Parameter] public int? EventMaxStack { get; set; }
        [Parameter] public string MoreLinkClick { get; set; }
        [Parameter] public DateTimeFormatter DayPopoverFormat { get; set; }
        // [Parameter] public Func<int, string, IEnumerable<string>> OnMoreLinkClassNames { get; set; } // Todo
        // [Parameter] public Func<int, string, object> OnMoreLinkContent { get; set; } // Todo: Replace object with proper type // Todo
        [Parameter] public Action<int, string> OnMoreLinkDidMount { get; set; }
        [Parameter] public Action<int, string> OnMoreLinkWillUnmount { get; set; }

        #endregion

        #region International

        [Parameter] public string Locale { get; set; }
        [Parameter] public string Direction { get; set; }
        [Parameter] public DayOfWeek? FirstDay { get; set; }

        #endregion

        // JSInvokable methods

        #region Overall Display

        [JSInvokable] public void WindowResize(View view) => OnWindowResize?.Invoke(view); // Todo: Replace object with View type

        #endregion

        #region Views

        // [JSInvokable] public IEnumerable<string> AllDayClassNames(object arg) => OnAllDayClassNames?.Invoke(arg) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object AllDayContent(object arg) => OnAllDayContent?.Invoke(arg); // Todo
        [JSInvokable] public void AllDayDidMount(string text) => OnAllDayDidMount?.Invoke(text);
        [JSInvokable] public void AllDayWillUnmount(string text) => OnAllDayWillUnmount?.Invoke(text);
        // [JSInvokable] public IEnumerable<string> NoEventsClassNames(object arg) => OnNoEventsClassNames?.Invoke(arg) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object NoEventsContent(object arg) => OnNoEventsContent?.Invoke(arg); // Todo
        [JSInvokable] public void NoEventsDidMount(object el) => OnNoEventsDidMount?.Invoke(el);
        [JSInvokable] public void NoEventsWillUnmount(object el) => OnNoEventsWillUnmount?.Invoke(el);
        // [JSInvokable] public IEnumerable<string> ViewClassNames(object arg) => OnViewClassNames?.Invoke(arg) ?? Enumerable.Empty<string>(); // Todo
        [JSInvokable] public void ViewDidMount(View view, object el) => OnViewDidMount?.Invoke(view, el);
        [JSInvokable] public void ViewWillUnmount(View view, object el) => OnViewWillUnmount?.Invoke(view, el);

        #endregion Views

        #region Date and Time

        [JSInvokable] public void DayHeaderDidMount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderDidMount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void DayHeaderWillUnmount(DayHeaderRenderInfo dayHeaderRenderInfo) => OnDayHeaderWillUnmount?.Invoke(dayHeaderRenderInfo);
        [JSInvokable] public void DayCellDidMount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellDidMount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void DayCellWillUnmount(DayCellRenderInfo dayCellRenderInfo) => OnDayCellWillUnmount?.Invoke(dayCellRenderInfo);
        [JSInvokable] public void SlotLabelDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLabelDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLabelWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLabelWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLaneDidMount(SlotRenderInfo slotRenderInfo) => OnSlotLaneDidMount?.Invoke(slotRenderInfo);
        [JSInvokable] public void SlotLaneWillUnmount(SlotRenderInfo slotRenderInfo) => OnSlotLaneWillUnmount?.Invoke(slotRenderInfo);
        [JSInvokable] public void DatesSet(DateInfo dateInfo) => OnDatesSet?.Invoke(dateInfo);
        [JSInvokable] public void WeekNumberDidMount(string num, string text, DateTime date) => OnWeekNumberDidMount?.Invoke(num, text, date);
        [JSInvokable] public void WeekNumberWillUnmount(string num, string text, DateTime date) => OnWeekNumberWillUnmount?.Invoke(num, text, date);
        [JSInvokable] public void SelectAllow(SelectInfo selectInfo) => OnSelectAllow?.Invoke(selectInfo);
        [JSInvokable] public void NowIndicatorDidMount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorDidMount?.Invoke(nowIndicatorInfo);
        [JSInvokable] public void NowIndicatorWillUnmount(NowIndicatorInfo nowIndicatorInfo) => OnNowIndicatorWillUnmount?.Invoke(nowIndicatorInfo);

        #endregion

        #region Event

        [JSInvokable] public void EventAdd(EventAddInfo eventAddInfo) => OnEventAdd?.Invoke(eventAddInfo);
        [JSInvokable] public void EventRemove(EventChangeInfo eventChangeInfo) => OnEventChange?.Invoke(eventChangeInfo);
        [JSInvokable] public void EventChange(EventAddInfo eventRemoveInfo) => OnEventRemove?.Invoke(eventRemoveInfo);
        [JSInvokable] public void EventsSet(IEnumerable<Event> events) => OnEventsSet?.Invoke(events);
        // [JSInvokable] public IEnumerable<string> EventClassNames(EventRenderInfo eventRenderInfo) => OnEventClassNames?.Invoke(eventRenderInfo) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object EventContent(EventRenderInfo eventRenderInfo) => OnEventContent?.Invoke(eventRenderInfo); // Todo
        [JSInvokable] public void EventDidMount(EventRenderInfo eventRenderInfo) => OnEventDidMount?.Invoke(eventRenderInfo);
        [JSInvokable] public void EventWillUnmount(EventRenderInfo eventRenderInfo) => OnEventWillUnmount?.Invoke(eventRenderInfo);
        [JSInvokable] public void EventClick(EventClickInfo eventClickInfo) => OnEventClick?.Invoke(eventClickInfo);
        [JSInvokable] public void EventMouseEnter(EventClickInfo mouseEnterInfo) => OnEventMouseEnter?.Invoke(mouseEnterInfo);
        [JSInvokable] public void EventMouseLeave(EventClickInfo mouseLeaveInfo) => OnEventMouseLeave?.Invoke(mouseLeaveInfo);
        [JSInvokable] public bool EventOverlap(Event stillEvent, Event movingEvent) => OnEventOverlap?.Invoke(stillEvent, movingEvent) ?? true;
        [JSInvokable] public bool EventAllow(EventAllowInfo eventAllowInfo, Event draggedEvent) => OnEventAllow?.Invoke(eventAllowInfo, draggedEvent) ?? true;
        [JSInvokable] public bool DropAccept(object draggableItem) => OnDropAccept?.Invoke(draggableItem) ?? true; // Todo: Replace object with DraggableItem type.
        [JSInvokable] public void EventDragStart(EventDragInfo eventDragInfo) => OnEventDragStart?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDragStop(EventDragInfo eventDragInfo) => OnEventDragStop?.Invoke(eventDragInfo);
        [JSInvokable] public void EventDrop(EventDropInfo eventDropInfo) => OnEventDrop?.Invoke(eventDropInfo);
        [JSInvokable] public void Drop(DropInfo dropInfo) => OnDrop?.Invoke(dropInfo);
        [JSInvokable] public void EventReceive(ExternalEventDropInfo eventReceiveInfo) => OnEventReceive?.Invoke(eventReceiveInfo);
        [JSInvokable] public void EventLeave(ExternalEventDropInfo eventLeaveInfo) => OnEventLeave?.Invoke(eventLeaveInfo);
        [JSInvokable] public void EventResizeStart(EventDragInfo eventResizeInfo) => OnEventResizeStart?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResizeStop(EventDragInfo eventResizeInfo) => OnEventResizeStop?.Invoke(eventResizeInfo);
        [JSInvokable] public void EventResize(EventResizeInfo eventResizeInfo) => OnEventResize?.Invoke(eventResizeInfo);
        // [JSInvokable] public IEnumerable<string> MoreLinkClassNames(int num, string text) => OnMoreLinkClassNames?.Invoke(num, text) ?? Enumerable.Empty<string>(); // Todo
        // [JSInvokable] public object MoreLinkContent(int num, string text) => OnMoreLinkContent?.Invoke(num, text); // Todo
        [JSInvokable] public void MoreLinkDidMount(int num, string text) => OnMoreLinkDidMount?.Invoke(num, text);
        [JSInvokable] public void MoreLinkWillUnmount(int num, string text) => OnMoreLinkWillUnmount?.Invoke(num, text);

        #endregion

        // Lifecycle methods
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var data = new FullCalendarData
            {
                #region Overall Display

                HeaderToolbar = HeaderToolbar,
                FooterToolbar = FooterToolbar,
                TitleFormat = TitleFormat,
                TitleRangeSeparator = TitleRangeSeparator,
                ButtonText = ButtonText,
                ButtonIcons = ButtonIcons,
                CustomButtons = CustomButtons,
                ThemeSystem = ThemeSystem,
                Height = Height,
                ContentHeight = ContentHeight,
                AspectRatio = AspectRatio,
                ExpandRows = ExpandRows,
                HandleWindowResize = HandleWindowResize,
                WindowResizeDelay = WindowResizeDelay,
                StickyHeaderDates = StickyHeaderDates,
                StickyFooterScrollbar = StickyFooterScrollbar,

                #endregion

                #region Views

                FixedWeekCount = FixedWeekCount,
                ShowNonCurrentDates = ShowNonCurrentDates,
                EventMinHeight = EventMinHeight,
                EventShortHeight = EventShortHeight,
                SlotEventOverlap = SlotEventOverlap,
                AllDaySlot = AllDaySlot,
                ListDayFormat = ListDayFormat,
                ListDaySideFormat = ListDaySideFormat,
                InitialView = InitialView,

                #endregion

                #region Date and Time

                Weekends = Weekends,
                HiddenDays = HiddenDays,
                DayHeaders = DayHeaders,
                DayHeaderFormat = DayHeaderFormat,
                DayMinWidth = DayMinWidth,
                SlotDuration = SlotDuration,
                SlotLabelInterval = SlotLabelInterval,
                SlotLabelFormat = SlotLabelFormat,
                SlotMinTime = SlotMinTime,
                SlotMaxTime = SlotMaxTime,
                ScrollTime = ScrollTime,
                ScrollTimeReset = ScrollTimeReset,
                InitialDate = InitialDate,
                DateIncrement = DateIncrement,
                DateAlignment = DateAlignment,
                ValidRange = ValidRange,
                NavLinks = NavLinks,
                NavLinkDayClick = NavLinkDayClick,
                NavLinkWeekClick = NavLinkWeekClick,
                WeekNumbers = WeekNumbers,
                WeekNumberCalculation = WeekNumberCalculation,
                WeekText = WeekText,
                WeekNumberFormat = WeekNumberFormat,
                Selectable = Selectable,
                SelectMirror = SelectMirror,
                UnselectAuto = UnselectAuto,
                UnselectCancel = UnselectCancel,
                SelectOverlap = SelectOverlap,
                SelectConstraint = SelectConstraint,
                SelectMinDistance = SelectMinDistance,
                NowIndicator = NowIndicator,
                Now = Now,
                BusinessHours = BusinessHours,

                #endregion

                #region Event

                Events = Events,
                DefaultAllDay = DefaultAllDay,
                DefaultAllDayEventDuration = DefaultAllDayEventDuration,
                DefaultTimedEventDuration = DefaultTimedEventDuration,
                ForceEventDuration = ForceEventDuration,
                EventColor = EventColor,
                EventBackgroundColor = EventBackgroundColor,
                EventBorderColor = EventBorderColor,
                EventTextColor = EventTextColor,
                EventDisplay = EventDisplay,
                EventTimeFormat = EventTimeFormat,
                DisplayEventTime = DisplayEventTime,
                DisplayEventEnd = DisplayEventEnd,
                NextDayThreshold = NextDayThreshold,
                EventOrder = EventOrder,
                EventOrderStrict = EventOrderStrict,
                ProgressiveEventRendering = ProgressiveEventRendering,
                Editable = Editable,
                EventStartEditable = EventStartEditable,
                EventResizableFromStart = EventResizableFromStart,
                EventDurationEditable = EventDurationEditable,
                EventResourceEditable = EventResourceEditable,
                Droppable = Droppable,
                EventDragMinDistance = EventDragMinDistance,
                DragRevertDuration = DragRevertDuration,
                DragScroll = DragScroll,
                SnapDuration = SnapDuration,
                AllDayMaintainDuration = AllDayMaintainDuration,
                EventConstraint = EventConstraint,
                DayMaxEventRows = DayMaxEventRows,
                DayMaxEvents = DayMaxEvents,
                EventMaxStack = EventMaxStack,
                MoreLinkClick = MoreLinkClick,
                DayPopoverFormat = DayPopoverFormat,

                #endregion

                #region International

                Locale = Locale,
                Direction = Direction,
                FirstDay = FirstDay,

                #endregion
            };
            await JsInterop.Render(Id, data, DotNetObjectReference.Create(this));
        }
    }
}