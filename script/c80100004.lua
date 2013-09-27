--セイバー･シャーク
function c80100004.initial_effect(c)
	--lv change
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80100004,0))
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetType(EFFECT_TYPE_IGNITION)
	e1:SetRange(LOCATION_MZONE)
	e1:SetCountLimit(2)
	e1:SetCost(c80100004.cost)
	e1:SetTarget(c80100004.target)
	e1:SetOperation(c80100004.operation)
	c:RegisterEffect(e1)
	--not synchro material
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_SINGLE)
	e2:SetCode(EFFECT_CANNOT_BE_SYNCHRO_MATERIAL)
	e2:SetProperty(EFFECT_FLAG_CANNOT_DISABLE+EFFECT_FLAG_UNCOPYABLE)
	e2:SetValue(1)
	c:RegisterEffect(e2)
	if not c80100004.global_check then
		c80100004.global_check=true
		local ge1=Effect.CreateEffect(c)
		ge1:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
		ge1:SetCode(EVENT_SPSUMMON_SUCCESS)
		ge1:SetOperation(c80100004.checkop)
		Duel.RegisterEffect(ge1,0)
		local ge2=Effect.CreateEffect(c)
		ge2:SetType(EFFECT_TYPE_FIELD+EFFECT_TYPE_CONTINUOUS)
		ge2:SetCode(EVENT_PHASE_START+PHASE_DRAW)
		ge2:SetOperation(c80100004.clear)
		Duel.RegisterEffect(ge2,0)
	end
end
function c80100004.checkop(e,tp,eg,ep,ev,re,r,rp)
	local tc=eg:GetFirst()
	while tc do
		if not tc:IsAttribute(ATTRIBUTE_WATER) then
			c80100004[tc:GetSummonPlayer()]=false
		end
		tc=eg:GetNext()
	end
end
function c80100004.clear(e,tp,eg,ep,ev,re,r,rp)
	c80100004[0]=true
	c80100004[1]=true
end
function c80100004.cost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return c80100004[tp] end
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET+EFFECT_FLAG_OATH)
	e1:SetCode(EFFECT_CANNOT_SPECIAL_SUMMON)
	e1:SetReset(RESET_PHASE+PHASE_END)
	e1:SetTargetRange(1,0)
	e1:SetTarget(c80100004.splimit)
	Duel.RegisterEffect(e1,tp)
end
function c80100004.splimit(e,c)
	return not c:IsAttribute(ATTRIBUTE_WATER)
end
function c80100004.filter(c)
	return c:IsFaceup() and c:IsRace(RACE_FISH)
end
function c80100004.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsLocation(LOCATION_MZONE) and c80100004.filter(chkc) end
	if chk==0 then return Duel.IsExistingTarget(c80100004.filter,tp,LOCATION_MZONE,LOCATION_MZONE,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_FACEUP)
	local g=Duel.SelectTarget(tp,c80100004.filter,tp,LOCATION_MZONE,LOCATION_MZONE,1,1,nil)
	local tc=g:GetFirst()
	local op=0
	if tc:GetLevel()==1 then op=Duel.SelectOption(tp,aux.Stringid(80100004,1))
	else op=Duel.SelectOption(tp,aux.Stringid(80100004,1),aux.Stringid(80100004,2)) end
	e:SetLabel(op)
end
function c80100004.operation(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	local tc=Duel.GetFirstTarget()
	if tc:IsFaceup() and tc:IsRelateToEffect(e) then
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetProperty(EFFECT_FLAG_CANNOT_DISABLE)
		e1:SetCode(EFFECT_UPDATE_LEVEL)
		e1:SetReset(RESET_EVENT+0x1fe0000)
		if e:GetLabel()==0 then
			e1:SetValue(1)
		else e1:SetValue(-1) end
		tc:RegisterEffect(e1)
	end
end