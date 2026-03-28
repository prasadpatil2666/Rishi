# Viewdly Platform - Quick Start Guide

## What You Get

An attractive, fully functional Blazor WebAssembly application featuring:

### ✅ Search Functionality
- **Massive search bar** that dominates the hero section
- Real-time search as you type
- Non-mandatory category filtering
- Context-aware quick search suggestions

### ✅ Dynamic Category Filter
Categories available:
- 🏢 Company Reviews
- 📱 Mobile Devices
- 💻 Technology
- ⚙️ Services
- 🛒 Retail
- 🍽️ Restaurants
- ✈️ Travel

The category filter automatically updates the "Popular searches" based on your selection.

### ✅ Review Feed
- Beautiful review cards with verified badges
- 5-star ratings
- Reviewer information and helpful count
- Action buttons (Helpful, Reply, Report)
- Real-time filtering

### ✅ Request Help Feature
- Modal dialog for users with bad experiences
- Captures: Company name, user info, issue description
- Success messaging with reference ID
- Form validation

### ✅ Sidebar Widgets
1. Request Help widget - Quick access to mediation
2. Community Stats - Shows platform metrics
3. Trust Features - Explains why Viewdly is trustworthy

## Features in Action

### Example 1: Search for "Microsoft"
1. Type "Microsoft" in the search bar
2. See all Microsoft-related reviews instantly
3. Click "Company Reviews" to filter further
4. See only verified Microsoft company reviews

### Example 2: Browse Mobile Devices
1. Click the "📱 Mobile Devices" category
2. Search suggestions change to: iPhone 15 Pro Max, Samsung Galaxy S24, etc.
3. Type any of these to see device reviews
4. Category label stays visible at the top of results

### Example 3: Request Help
1. Click "Request Mediation" button in sidebar
2. Fill in the form with details about bad experience
3. Submit to get a reference ID
4. Our mediation team will contact you

## Search Examples to Try

**Company Reviews:**
- Microsoft
- Google
- Amazon

**Mobile Devices:**
- iPhone 15 Pro Max
- Samsung Galaxy S24
- Pixel 8 Pro

**Technology:**
- MacBook Pro
- Dell XPS
- iPad Pro

## UI/UX Highlights

### Hero Section
- Purple gradient background for visual impact
- Large, clear hero title
- Subtitle explaining the platform
- Three-part search interface

### Search Bar Design
- 🔍 Search icon
- Input field with placeholder
- Search button
- Clean, minimal aesthetic

### Category Buttons
- Icon + text for clarity
- Active state highlighting
- Clear button to reset filter
- Smooth hover effects

### Review Cards
- Clean white cards on gray background
- Avatar with user initials
- Verified badge for trusted reviewers
- Star ratings clearly visible
- Engagement metrics (helpful count)
- Action buttons for interaction

### Responsive Design
- Desktop: Full sidebar layout
- Tablet: Sidebar moves below reviews
- Mobile: Single column, easy to scroll

## Sample Data

The app comes with 6 sample reviews:
1. Microsoft workplace review (4.5 stars) - John D.
2. iPhone 15 Pro Max (5 stars) - Sarah M.
3. Google workplace review (4 stars) - Mike P.
4. Samsung Galaxy S24 (4.5 stars) - Alex K.
5. Starbucks review (3.5 stars) - Emma R.
6. MacBook Pro 14 (5 stars) - Dev J.

All marked as verified for demonstration.

## Color Scheme

**Primary Colors:**
- Deep Navy: #0f172a (text, primary elements)
- Bright Blue: #3b82f6 (accents, links, buttons)
- White: #ffffff (backgrounds, cards)

**Supporting Colors:**
- Success Green: #10b981 (verified badges)
- Warm Gold: #f59e0b (highlights)
- Neutral Grays: Various shades for text hierarchy

## CSS Classes Reference

Key classes used throughout:

- `.hero-search` - Hero section with search
- `.search-container` - Search box wrapper
- `.filter-section` - Category filter area
- `.reviews-grid` - Main layout grid
- `.review-card` - Individual review
- `.widget` - Sidebar widgets
- `.btn-primary` - Primary buttons
- `.modal-overlay` - Modal background
- `.form-group` - Form elements

## Browser Compatibility

- Chrome/Edge: ✅ Full support
- Firefox: ✅ Full support
- Safari: ✅ Full support
- Mobile browsers: ✅ Responsive design

## Performance Notes

- Instant search filtering (client-side)
- No server calls required for demo
- Fast modal animations
- Smooth scroll behavior
- Optimized CSS with variables

## Customization Tips

### To add more categories:
Edit the `categories` list in Home.razor

### To add more reviews:
Add entries to the `reviews` list initialization

### To change colors:
Modify CSS variables in `viewdly.css` `:root` section

### To modify common searches:
Update the `GetCommonSearches()` method in Home.razor

## Files Overview

```
StayFocus/
├── Pages/
│   └── Home.razor                 # Main page with search & reviews
├── Components/
│   ├── ReviewCard.razor           # Review card component
│   └── RequestHelpModal.razor     # Mediation modal
├── Models/
│   └── Review.cs                  # Data models
└── wwwroot/css/
    └── viewdly.css                # All styling (900+ lines)
```

## Running the App

```bash
# In the StayFocus directory
dotnet run

# Open browser to:
http://localhost:5000
```

## Next Steps

1. Try the search feature with different terms
2. Test category filtering
3. Explore the review cards
4. Test the Request Help modal
5. Try responsive design on different screen sizes
6. Customize colors and content as needed

Enjoy your Viewdly platform! 🚀
