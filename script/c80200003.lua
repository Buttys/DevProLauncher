--ギアギアタッカー
function c80200003.initial_effect(c)
	--turn set
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80200003,0))
	e1:SetCategory(CATEGORY_POSITION)
	e1:SetType(EFFECT_TYPE_IGNITION)
	e1:SetRange(LOCATION_MZONE)
	e1:SetTarget(c80200003.target)
	e1:SetOperation(c80200003.operation)
	c:RegisterEffect(e1)
	--destroy
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80200003,1))
	e2:SetCategory(CATEGORY_DESTROY)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e2:SetProperty(EFFECT_FLAG_DAMAGE_STEP)
	e2:SetCode(EVENT_FLIP)
	e2:SetTarget(c80200003.destg)
	e2:SetOperation(c80200003.desop)
	c:RegisterEffect(e2)
end
function c80200003.target(e,tp,eg,ep,ev,re,r,rp,chk)
	local c=e:GetHandler()
	if chk==0 then return c:IsCanTurnSet() and c:GetFlagEffect(80200003)==0 end
	c:RegisterFlagEffect(80200003,RESET_EVENT+0x1fc0000+RESET_PHASE+PHASE_END,0,1)
	Duel.SetOperationInfo(0,CATEGORY_POSITION,c,1,0,0)
end
function c80200003.operation(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	if c:IsRelateToEffect(e) and c:IsFaceup() then
		Duel.ChangePosition(c,POS_FACEDOWN_DEFENCE)
	end
end
function c80200003.cfilter(c)
	return c:IsFaceup() and c:IsSetCard(0x72)
end
function c80200003.desfilter(c)
	return c:IsType(TYPE_SPELL+TYPE_TRAP) and c:IsDestructable()
end
function c80200003.destg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return Duel.IsExistingMatchingCard(c80200003.cfilter,tp,LOCATION_MZONE,0,1,e:GetHandler())
		and Duel.IsExistingMatchingCard(c80200003.desfilter,tp,LOCATION_ONFIELD,LOCATION_ONFIELD,1,nil) end
	local g=Duel.GetMatchingGroup(c80200003.desfilter,tp,LOCATION_ONFIELD,LOCATION_ONFIELD,nil)
	Duel.SetOperationInfo(0,CATEGORY_DESTROY,g,1,0,0)
end
function c80200003.desop(e,tp,eg,ep,ev,re,r,rp)
	local ct=Duel.GetMatchingGroupCount(c80200003.cfilter,tp,LOCATION_MZONE,0,e:GetHandler())
	local g=Duel.GetMatchingGroup(c80200003.desfilter,tp,LOCATION_ONFIELD,LOCATION_ONFIELD,nil)
	if ct>0 and g:GetCount()>0 then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_DESTROY)
		local dg=g:Select(tp,1,ct,nil)
		Duel.HintSelection(dg)
		Duel.Destroy(dg,REASON_EFFECT)
	end
end
