class corsMiddleware(object):
    def process_response(self, req, resp):
        resp["Access-Control-Allow-Headers"] = "Authentication, Content-Type, X-CSRFToken"
        resp["Access-Control-Allow-Origin"] = req.META["Host"]
        return resp
