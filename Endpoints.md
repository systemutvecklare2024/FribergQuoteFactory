# Endpoints

| Action                                    | HTTP   | Route                          |
| ----------------------------------------- | ------ | ------------------------------ |
| Get random quote (optionally by category) | GET    | `/api/quotes/random?category=` |
| Submit new quote                          | POST   | `/api/quotes`                  |
| Get all unapproved quotes                 | GET    | `/api/quotes/unapproved`       |
| Approve a quote                           | PUT    | `/api/quotes/{id}/approve`     |
